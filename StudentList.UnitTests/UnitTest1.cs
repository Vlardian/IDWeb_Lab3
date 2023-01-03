using StudentList.Controllers;
using StudentList.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StudentList.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var connectionstring = "Data Source=VLARDIAN;Initial Catalog=StudentList;Trusted_Connection=True;";
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(connectionstring);

            using (var db = new ApplicationDbContext(optionsBuilder.Options))
            {
                var studentController = new StudentController(db);

                Assert.AreEqual(1, studentController.GetStudentById(1).Id); // verficam daca metoda GetStudentById cu argumentul 1 returneaza Id 1
            }
            
        }

        [TestMethod]
        public void TestMethod2()
        {
            var connectionstring = "Data Source=VLARDIAN;Initial Catalog=StudentList;Trusted_Connection=True;";
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(connectionstring);

            using (var db = new ApplicationDbContext(optionsBuilder.Options))
            {
                var studentController = new StudentController(db);

                Assert.AreEqual("Ion Ciobanu", studentController.GetStudentByName("Ion Ciobanu").Name);
            }

        }

        [TestMethod]
        public void TestMethod3()
        {
            var connectionstring = "Data Source=VLARDIAN;Initial Catalog=StudentList;Trusted_Connection=True;";
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(connectionstring);

            using (var db = new ApplicationDbContext(optionsBuilder.Options))
            {
                var studentController = new StudentController(db);

                Assert.AreEqual(true, studentController.GetStudentByFaculty("FCIM").Faculty.Equals("FCIM"));
            } 

        }
    }
}
