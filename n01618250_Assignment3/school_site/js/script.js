window.onload = pageReady;
function pageReady() {
  var teacherLists = document.getElementById("teacherLists");
  let url = "http://localhost:14272/api/TeacherData/ListTeachers";

  let xhttp = new XMLHttpRequest();

  xhttp.onreadystatechange = function () {
    console.log(this.readyState);
    console.log(this.status);
    //ready state should be 4 and 200
    if (this.readyState == 4 && this.status == 200) {
      //request successful
      console.log(this.response);
      for (var i = 0; i < this.response.length; i++) {
        teacherLists.innerHTML += "<li><a href='#'>" + this.response[i].TeacherFName + " " + this.response[i].TeacherLName + "</a></li>";
      }
    }
  };

  xhttp.responseType = "json";
  xhttp.open("GET", url, true);

  xhttp.send();
};