 function uservalid() {
    var empname, midname, lastname, initial, username, password, conpass, dob, gender, add, country, state, city, mobileno, telno, emailid, emailexp, zipcode, branchid;

    empname = document.getElementById("txtempname").value;
    midname = document.getElementById("txtmidname").value;
    lastname = document.getElementById("txtlastname").value;
    initial = document.getElementById("txtinitial").value;
    username = document.getElementById("txtusername").value;
    password = document.getElementById("txtpassword").value;
    conpass = document.getElementById("txtconpass").value;
    dob = document.getElementById("txtdob").value;
    gender = document.getElementById("txtgender").value;
    add = document.getElementById("txtaddress").value;
    country = document.getElementById("txtcountry").value;
    state = document.getElementById("txtstate").value;
    city = document.getElementById("txtcity").value;
    mobileno = document.getElementById("txtmobileno").value;
    telno = document.getElementById("txttelno").value;
    emailid = document.getElementById("txtemailid").value;
    emailexp = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([com\co\.\in])+$/;
    zipcode = document.getElementById("txtzipcode").value;
    branchid = document.getElementById("txtbranch").value;

    if (empname == '' && midname == '' && lastname == '' && initial == '' && username == '' && password == '' && conpass == '' && dob == '' && gender == '' && add == '' && country == '' && state == '' && city == '' && mobileno == '' && telno == '' && emailid == '' && zipcode == '' && branchid == '') {
        alert("Enter All Fields");
        return false;
    }

    if (password != conpass) {
        alert("Password Doesn't Match");
        return false;
    }

    if (emailid != '') {
        if (!emailid.match(emailexp)) {
            alert("Invalid Email Id");
            return false;
        }
    }

    if (empname == '') {
        alert("Enter First Name");
        return false;
    }
    if (midname == '') {
        alert("Enter Last Name");
        return false;
    }
    if (lastname == '') {
        alert("Enter Last Name");
        return false;
    }
    if (initial == '') {
        alert("Enter Initial");
        return false;
    }
    if (username == '') {
        alert("Enter User Name");
        return false;
    }
    if (password == '') {
        alert("Enter User Name");
        return false;
    }
    if (password != '' && confirm == '') {
        alert("Please Confirm Password");
        return false;
    }
    if (dob == '') {
        alert("Enter Date Of Birth");
        return false;
    }
    if (gender == '') {
        alert("Please Enter Gender");
        return false;
    }
    if (add == '') {
        alert("Please Enter Address");
        return false;
    }
    if (country == '') {
        alert("Please Enter Country");
        return false;
    }
    if (state == '') {
        alert("Please Enter State");
        return false;
    }
    if (city == '') {
        alert("Please Enter City");
        return false;
    }
    if (mobileno== '') {
        alert("Please Enter Mobile No");
        return false;
    }
    if (telno == '') {
        alert("Please Enter Phone No");
        return false;
    }
    if (zipcode == '') {
        alert("Please Enter Zip Code");
        return false;
    }
    if (branchid == '') {
        alert("Please Select Branch Name");
        return false;
    }
    
}