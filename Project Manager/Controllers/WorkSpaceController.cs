using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_Manager.Models;
using ProjectManage.Models;
using StockMachine.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Project_Manager.Controllers
{
    public class WorkSpaceController : Controller
    {
        LoginEntities2 _context = new LoginEntities2();
        // GET: WorkSpace
        public ActionResult WorkSpaces()
        {
            LoginEntities2 DisplayProjects = new LoginEntities2();
            String Username = Session["UserName"].ToString();
            ViewBag.Name = Username;
            var list = DisplayProjects.ProjectNames.Where(m => m.UserName == Username).ToList();
            return View(list);
        }
        public ActionResult Add_WorkSpace()
        {
            return View();
        }
        public ActionResult Create_WorkSpace()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create_WorkSpace(ProjectName project,string Input)
        {
            LoginEntities2 Workspace = new LoginEntities2();
            project.UserName = Session["UserName"].ToString();
            project.ProjectName1 = Input;
            Workspace.ProjectNames.Add(project);
            try
            {
                Workspace.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
            return View("WorkSpaces");
        }
        public ActionResult Projects()
        {
            return View();
        }
        public ActionResult DocumentList()
        {
            var list = _context.Files.ToList();
            return View(list);
        }
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Index(string fileText, IFormFile files)
        {

            if (files != null)
            {
                if (files.Length > 0)
                {
                    //Getting FileName
                    var fileName = Path.GetFileName(files.FileName);
                    //Getting file Extension
                    var fileExtension = Path.GetExtension(fileName);
                    // concatenating  FileName + FileExtension
                    var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);

                    var objfiles = new Files()
                    {
                        DocumentId = 0,
                        Name = fileName,

                        FileType = fileExtension,
                        CreatedOn = DateTime.Now,
                        FileText = fileText
                    };

                    using (var target = new MemoryStream())
                    {
                        files.CopyTo(target);
                        objfiles.DataFiles = target.ToArray();

                        //objfiles.DataFiles = target.ToArray();
                    }

                    _context.Files.Add(objfiles);
                    _context.SaveChanges();

                }
            }
            return View();
        }
        [AllowAnonymous]
        public ActionResult AddTagType()
        {
            ViewBag.Project = _context.Projects.ToList();
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult AddTagType(TagTypes type)
        {
            if (type != null)
            {

                _context.TagTypes.Add(type);
                _context.SaveChanges();
            }
            ViewBag.Project = _context.Projects.ToList();
            return View();
        }
        //////
        ///
        [AllowAnonymous]
        public ActionResult EditTagType(int Id)
        {
            ViewBag.Project = _context.Projects.ToList();
            var tag = _context.TagTypes.Where(x => x.Id == Id).FirstOrDefault();
            return View(tag);
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult EditTagType(TagTypes type)
        {
            if (type != null)
            {
                var tagType = _context.TagTypes.Where(x => x.Id == type.Id).FirstOrDefault();
                if (tagType != null)
                {
                    tagType.TagType = type.TagType;
                    tagType.fk_Project = type.fk_Project;
                    _context.SaveChanges();

                }
            }
            ViewBag.Project = _context.Projects.ToList();
            return View();
        }
        ///
        /// 
        /// 
        [AllowAnonymous]
        [HttpGet]
        public ActionResult TagType(int? Id)
        {
            ViewBag.Project = _context.Projects.ToList();
            if (Id > 0)
            {
                var tagType = _context.TagTypes.Where(x => x.fk_Project == Id).ToList();

                return View(tagType);
            }
            else
            {
                var tagType = _context.TagTypes.ToList();
                return View(tagType);
            }
        }

        //////
        ///
        ///
        [AllowAnonymous]
        public ActionResult EditDocument(int Id)
        {
            var file = _context.Files.Where(x => x.DocumentId == Id).FirstOrDefault();
            return View(file);
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult EditDocument(int DocumentId, string FileText, IFormFile DataFiles)
        {
            if (DataFiles != null)
            {
                //Getting FileName
                var fileName = Path.GetFileName(DataFiles.FileName);
                //Getting file Extension
                var fileExtension = Path.GetExtension(fileName);
                // concatenating  FileName + FileExtension
                var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);

                var objfiles = new Files()
                {
                    DocumentId = 0,
                    Name = FileText,
                    FileType = fileExtension,
                    CreatedOn = DateTime.Now,
                    FileText = FileText
                };

                using (var target = new MemoryStream())
                {
                    DataFiles.CopyTo(target);
                    objfiles.DataFiles = target.ToArray();

                    //objfiles.DataFiles = target.ToArray();
                }
                var tagType = _context.Files.Where(x => x.DocumentId == DocumentId).FirstOrDefault();
                if (tagType != null)
                {


                    using (var target = new MemoryStream())
                    {
                        DataFiles.CopyTo(target);
                        objfiles.DataFiles = target.ToArray();
                        //objfiles.DataFiles = target.ToArray();
                    }
                    tagType.DataFiles = objfiles.DataFiles;
                    tagType.Name = objfiles.Name;
                    tagType.FileText = objfiles.FileText;
                    tagType.FileType = objfiles.FileType;
                    _context.SaveChanges();
                }
            }
            return View();
        }
        ////
        ///
        [AllowAnonymous]
        public ActionResult SearchTagType(int Id)
        {
            var file = _context.TagTypes.Where(x => x.fk_Project == Id);
            return View(file);
        }
    }
}