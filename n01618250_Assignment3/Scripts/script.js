@Scripts.Render("~/Scripts/script.js")
    alert("HEllo");
    $(document).ready(function () {
        $("form").click(function () {

            var validation = $("#FrmIndex"); // My From Id
            if (!validation.valid()) {
                return false;
            }

        });
    });
    var submitBtn = document.getElementById("submit-btn");
submitBtn.addEventListener((click), (e) => {
    e.preventDefault();
    var firstName = document.getElementById('TeacherFname').value;
    var lastName = document.getElementById('TeacherLname').value;
    var salary = document.getElementById('TeacherSalary').value;
    var date = document.getElementById('TeacherHireDate').value;

    if (firstName.trim() === '' || lastName.trim() === '') {
        alert('First name and last name are required!');
        return false;
    }

    if (isNaN(parseFloat(salary)) || !isFinite(salary)) {
        alert('Salary must be a valid number!');
        return false;
    }
    
});