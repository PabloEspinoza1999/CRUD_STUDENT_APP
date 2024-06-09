using Microsoft.AspNetCore.Mvc;
using APPLICATION_WEB_LAB_INV.Models;
using Microsoft.AspNetCore.Http;
using INV_CASSANDRA.Reposity;

namespace INV_CASSANDRA.Controllers
{
    public class StudentController : Controller
    {

        private readonly StudentReposity _Context;
        //Servicio
        public StudentController(StudentReposity Context)
        {
            _Context = Context;
        }

        // GET: Student
        public ActionResult Index()
        {
            var students = _Context.GetAllStudents();
            return View(students);
        }


        // GET: StudentController/Details/5
        public ActionResult Details(Guid id)
        {
            var student = _Context.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _Context.InsertStudent(student);
                    return RedirectToAction(nameof(Index));
                }
                return View(student);
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var student = _Context.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _Context.UpdateStudent(student);
                    return RedirectToAction(nameof(Index));
                }
                return View(student);
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var student = _Context.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                _Context.DeleteStudent(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
