using CRUDapiConsumptionInMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace CRUDapiConsumptionInMVC.Controllers
{
    public class StudentController : Controller
    {
        //getting root API in string variable in MVC(client) part
        private string url = "https://localhost:44338/api/StudentsApi/";

        //creating object of HttpClient object to work with URLs/APIs in MVC
        private HttpClient client = new HttpClient();

        //it will hit GET: /api/StudentsApi => to give list of all students
        [HttpGet]
        public IActionResult Index()
        {
            //creating List of students
            List<Student> students = new List<Student>();

            //hitting the URL with GET method using GetAsync() of HttpClient object, and
            //capturing the response, i.e data and status code

            //can also be done in asynchronous way as =>
            //HttpResponseMessage response = await client.GetAsync(url);

            //this shows synchronous way
            HttpResponseMessage response = client.GetAsync(url).Result;

            //If 200 Success code received,
            //then read the JSON response as string
            if (response.IsSuccessStatusCode)
            {
                //Reading the JSON response received from HttpClient as string, and storing in result variable
                //can also be done in asynchronous way as =>
                //string result = await response.Content.ReadAsStringAsync();

                //this shows synchronous way
                string result = response.Content.ReadAsStringAsync().Result;

                //converting i.e. Deserializing result(that has JSON data) into List of students and storing it in data variable
                var data = JsonConvert.DeserializeObject<List<Student>>(result);

                if(data != null)
                {
                    students = data;
                }
            }
            else
            {
                // Log or handle the error case
                ModelState.AddModelError(string.Empty, "Failed to load data.");
            }
            return View(students);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //it will hit POST: /api/StudentsApi/ => to create new student data
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student std)
        {
            //Serialize the data received in "std" object as JSON
            string data = JsonConvert.SerializeObject(std);

            //changing JSON to formatted text
            //StringContent class creates a formatted text appropriate for the http server/client communication
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            //this shows synchronous way to receive response as POST request
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            //Asynchronous way to receive response
            //HttpResponseMessage response = await client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                TempData["insert_message"] = "Student added..";
                return RedirectToAction("Index", "Student");
            }
            return View();
        }

        //first it will hit GET: /api/StudentsApi/{id} => to get list of particular student
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Student std = new Student();

            //getting student data for particular id 
            //synchronous way =>
            HttpResponseMessage response = client.GetAsync(url + id).Result;

            //asynchronous way => make method async Task<IActionResult> Edit(int id)
            //and HttpResponseMessage response = await client.GetAsync(url + id);

            if (response.IsSuccessStatusCode)
            {
                //Reading the JSON response received from HttpClient as string, and storing in result variable
                //can also be done in asynchronous way as =>
                //string result = await response.Content.ReadAsStringAsync();

                //this shows synchronous way
                string result = response.Content.ReadAsStringAsync().Result;

                //converting i.e. Deserializing result(that has JSON data) into List of students and storing it in data variable
                var data = JsonConvert.DeserializeObject<Student>(result);

                if (data != null)
                {
                    std = data;
                }
            }

            return View(std);
        }
        
        //then it will hit PUT: /api/StudentApi/ => to update the particular student details
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Student std)
        {
            //Serialize the data received in "std" object as JSON
            string data = JsonConvert.SerializeObject(std);

            //changing JSON to formatted text
            //StringContent class creates a formatted text appropriate for the http server/client communication
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            //using PUT: /api/StudentApi/{id} to edit/update particular student data
            //this shows synchronous way to receive response as POST request
            HttpResponseMessage response = client.PutAsync(url + std.id, content).Result;

            //Asynchronous way to receive response
            //HttpResponseMessage response = await client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                TempData["update_message"] = "Student updated..";
                return RedirectToAction("Index", "Student");
            }
            return View();
        }

        //it will use GET: /api/StudnetApi/{id}
        [HttpGet]
        public IActionResult Details(int id)
        {
            Student std = new Student();

            //getting student data for particular id 
            //synchronous way =>
            HttpResponseMessage response = client.GetAsync(url + id).Result;

            //asynchronous way => make method async Task<IActionResult> Edit(int id)
            //and HttpResponseMessage response = await client.GetAsync(url + id);

            if (response.IsSuccessStatusCode)
            {
                //Reading the JSON response received from HttpClient as string, and storing in result variable
                //can also be done in asynchronous way as =>
                //string result = await response.Content.ReadAsStringAsync();

                //this shows synchronous way
                string result = response.Content.ReadAsStringAsync().Result;

                //converting i.e. Deserializing result(that has JSON data) into List of students and storing it in data variable
                var data = JsonConvert.DeserializeObject<Student>(result);

                if (data != null)
                {
                    std = data;
                }
            }

            return View(std);
        }

        //first it will fetch particular student detail using GET: /api/StudentApi/{id}
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Student std = new Student();

            //getting student data for particular id 
            //synchronous way =>
            HttpResponseMessage response = client.GetAsync(url + id).Result;

            //asynchronous way => make method async Task<IActionResult> Edit(int id)
            //and HttpResponseMessage response = await client.GetAsync(url + id);

            if (response.IsSuccessStatusCode)
            {
                //Reading the JSON response received from HttpClient as string, and storing in result variable
                //can also be done in asynchronous way as =>
                //string result = await response.Content.ReadAsStringAsync();

                //this shows synchronous way
                string result = response.Content.ReadAsStringAsync().Result;

                //converting i.e. Deserializing result(that has JSON data) into List of students and storing it in data variable
                var data = JsonConvert.DeserializeObject<Student>(result);

                if (data != null)
                {
                    std = data;
                }
            }

            return View(std);
        }

        //it deletes particular student detail using DELETE: /api/StudentApi/{id}
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            //deleting student data for particular id 
            //synchronous way =>
            HttpResponseMessage response = client.DeleteAsync(url + id).Result;

            //asynchronous way => make method async Task<IActionResult> Edit(int id)
            //and HttpResponseMessage response = await client.GetAsync(url + id);

            if (response.IsSuccessStatusCode)
            {
                TempData["delete_message"] = "Student Deleted...";
                return RedirectToAction("Index", "Student");
            }

            return View();
        }
    }
}
