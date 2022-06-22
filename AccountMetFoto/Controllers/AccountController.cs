using Microsoft.AspNetCore.Mvc;
using AccountMetFoto.Models;
using AccountMetFoto.Database;
using AccountMetFoto.Domain;

namespace AccountMetFoto.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult About()
        {
            var vm = new AccountViewModel { FirstName = "Thomas", LastName = "Coppens", Address = "Schildersstraat 2, 9040 Gent", DOB = new DateOnly(1983, 04, 21), Gender = Gender.M };
            return View(vm);
        }

        private readonly IAccountDatabase accountDatabase;
        private readonly IWebHostEnvironment hostEnvironment;

        public AccountController(IAccountDatabase accountDatabase, IWebHostEnvironment hostEnvironment)
        {
            this.accountDatabase = accountDatabase;
            this.hostEnvironment = hostEnvironment;
        }

        private string UploadPhoto(IFormFile photo)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
            string pathName = Path.Combine(hostEnvironment.WebRootPath, "photos");
            string fileNameWithPath = Path.Combine(pathName, uniqueFileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                photo.CopyTo(stream);
            }
            return uniqueFileName;
        }

        private void DeletePhoto(string photoUrl)
        {
            string path = Path.Combine(hostEnvironment.WebRootPath, photoUrl.Substring(1));
            System.IO.File.Delete(path);
        }

        public IActionResult Overview()
        {
            var vm = accountDatabase.GetAccounts().Select(x => new AccountListViewModel
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Gender = x.Gender
            });
            return View(vm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create([FromForm] AccountCreateViewModel vm)
        {
            if (TryValidateModel(vm))
            {
                var account = new Account()
                {
                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    DOB = vm.DOB,
                    Address = vm.Address,
                    Gender = (Domain.Gender)vm.Gender
                };

                if (vm.Photo != null)
                {
                    string uniqueFileName = UploadPhoto(vm.Photo);
                    account.PhotoUrl = Path.Combine("/photos", uniqueFileName);
                }

                accountDatabase.Insert(account);
                return RedirectToAction(nameof(Overview));
            }
            return View();
        }

        [HttpGet]
        public IActionResult Detail([FromRoute] int Id)
        {
            var account = accountDatabase.GetAccount(Id);
            var vm = new AccountDetailViewModel
            {
                FirstName = account.FirstName,
                LastName = account.LastName,
                Gender = account.Gender,
                DOB = account.DOB,
                Address = account.Address,
                PhotoUrl= account.PhotoUrl
            };

            return View(vm);
        }


        [HttpGet]
        public IActionResult Edit([FromRoute] int Id)
        {
            var account = accountDatabase.GetAccount(Id);
            var vm = new AccountEditViewModel
            {
                FirstName = account.FirstName,
                LastName = account.LastName,
                Gender = account.Gender,
                DOB = account.DOB,
                Address = account.Address
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int Id, [FromForm] AccountEditViewModel vm)
        {
            if (TryValidateModel(vm))
            {
                var account = new Account()
                {
                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    Gender = vm.Gender,
                    DOB = vm.DOB,
                    Address = vm.Address
                };

                var accountFromDb = accountDatabase.GetAccount(Id);

                if (vm.Photo != null)
                {
                    if (!string.IsNullOrEmpty(accountFromDb.PhotoUrl))
                    {
                        DeletePhoto(accountFromDb.PhotoUrl);
                    }
                    string uniqueFileName = UploadPhoto(vm.Photo);
                    account.PhotoUrl = Path.Combine("/photos", uniqueFileName);
                }
                else
                {
                    account.PhotoUrl = accountFromDb.PhotoUrl;
                }

                accountDatabase.Update(Id, account);
                return RedirectToAction(nameof(Overview));
            }

            return View();
        }

        [HttpGet]
        public IActionResult Delete([FromRoute] int Id)
        {
            var account = accountDatabase.GetAccount(Id);

            var vm = new AccountDeleteViewModel
            {
                Id = account.Id,
                FirstName = account.FirstName,
                LastName = account.LastName,
                DOB = account.DOB,
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult ConfirmDelete([FromRoute] int Id)
        {
            accountDatabase.Delete(Id);
            return RedirectToAction(nameof(Overview));
        }

    }
}
