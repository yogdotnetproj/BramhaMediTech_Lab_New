 function NameValidation() {

    var userid;

    var controlId = document.getElementById("<%=txtbankname.ClientID %>");

    userid = controlId.value;

    var val = /^[a-zA-Z ]+$/

    if (userid == "") {

        return ("Please Enter Bank Name" + "\n");

    }

    else if (val.test(userid)) {

        return "";

    }

    else {

        return ("Name accepts only spaces and charcters" + "\n");

    }

}