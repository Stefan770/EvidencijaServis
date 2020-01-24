$(document).ready(() => {
    const host = window.location.host;
    let editingId;
    let token;
    let user;
    let headers = {};

    /*Main*/
    loadZaposleni();
    loadSelect();

    $("body").on("click", "#btnDel", deleteItem);
    $("body").on("click", "#btnEdt", editItem);
    $("body").on("click", "#btnOdustajanje", () => { $("#editFormDiv").hide(); });
    /*-----------------------------------------------------------*/

    /*Zaposleni--------------------------------------------------*/
    function loadZaposleni() {
        $.ajax({
            url: `http://${host}/api/zaposleni`,
            type: "GET",
            success: setZaposleni,
            beforeSend: () => { $("#loader-4").show(); },
            complete: () => { $("#loader-4").hide(); }
        });
    }

    function setZaposleni(data, status) {
        let table = $("<table class='table table-bordered'></table>");
        let h1 = $("<h2 class='m3'>Zaposleni</h2>");

        let head = $("<thead class='bg-warning'><tr><td>Ime i prezime</td><td>Godina rodjenja</td><td>Godina Zaposlenja</td><td>Kompanija</td></tr></thead>");
        if (token) { head.children().first().append("<td>Plata</td><td style='width: 10px'>Brisanje</td><td style='width: 10px'>Izmena</td>"); }
        table.append(head);
        let tbody = $("<tbody></tbody>");

        data.forEach((item, i) => {
            const row = $(`<tr><td>${item.ImeIPrezime}</td><td>${item.GodinaRodjenja}</td><td>${item.GodinaZaposlenja}</td><td>${item.Kompanija.Naziv}</td></tr>`);
            if (token) {
                row.append(`<td>${item.Plata}</td><td><button class="btn btn-default btn-sm" name=${item.Id} id="btnDel">Delete</button></td>
                            <td><button class="btn btn-default btn-sm" name=${item.Id} id="btnEdt">Edit</button></td>`);
            }
            tbody.append(row);
        });

        table.append(tbody);
        $("#data").empty().append(h1).append(table);

    }

    $("#filterForm").submit(e => {
        e.preventDefault();

        $.ajax({
            url: `http://${host}/api/zaposlenje`,
            type: "POST",
            headers: headers,
            data: {
                "Pocetak": $("#filterMin").val(),
                "Kraj": $("#filterMax").val()
            },
            success: setZaposleni
        }).fail(() => { alert("Greska prilikom pretrage!"); });
    });

    $("#editForm").submit(e => {
        e.preventDefault();

        const httpAction = "PUT";
        let url = `http://${host}/api/zaposleni/${editingId}`;
        let sendData = {
            "Id": editingId,
            "ImeIPrezime": $("#edtIme").val(),
            "GodinaRodjenja": $("#edtRodjenje").val(),
            "GodinaZaposlenja": $("#edtZaposlenje").val(),
            "Plata": $("#edtPlata").val(),
            "KompanijaId": $("#edtSelect").children("option:selected").val()
        };

        console.log("Objekat za slanje");
        console.log(sendData);

        $.ajax({
            url: url,
            type: httpAction,
            data: sendData,
            headers: headers

        }).done((data, status) => {
            if (status === "success") {
                loadZaposleni();
                $("#editFormDiv").hide();
            }
            else {
                alert("Greska prilikom izmene!");
            }

        }).fail((data, status) => {
            alert("Greska prilikom izmene!");
        });
    });


    function editItem() {
        const editId = this.name;

        $.ajax({
            url: `http://${host}/api/zaposleni/${editId}`,
            type: "GET"

        }).done((data, status) => {
            $("#edtIme").val(data.ImeIPrezime);
            $("#edtRodjenje").val(data.GodinaRodjenja);
            $("#edtZaposlenje").val(data.GodinaZaposlenja);
            $("#edtPlata").val(data.Plata);
            markSelected(data.KompanijaId);
            editingId = data.Id;
            $("#editFormDiv").show();

        }).fail((data, status) => {
            alert("Greska!");
        });
    }

    function deleteItem() {
        const deleteId = this.name;

        $.ajax({
            url: `http://${host}/api/zaposleni/${deleteId}`,
            type: "DELETE",
            headers: headers

        }).done((data, status) => {
            loadZaposleni();

        }).fail((data, status) => {
            alert("Desila se greska!");
        });
    }
    /*------------------------------------------------------------*/

    /*Kompanije---------------------------------------------------*/
    function loadSelect() {
        const url = `http://${host}/api/kompanije/`;
        $.getJSON(url, (data, status) => {
            $("#edtSelect").empty();
            data.forEach(e => {
                $("#edtSelect").append(`<option value="${e.Id}">${e.Naziv}</option>`);
            });
        });
    }

    function markSelected(selectedId) {
        $("#edtSelect").children().each(function (i) {
            $(this).removeAttr("selected");
            if ($(this).val() == selectedId) {
                $(this).attr("selected", "selected");
            }
        });
    }
    /*------------------------------------------------------------*/

    /*Authentication----------------------------------------------*/

    $("#btnReg").click(() => {

        const sendData = {
            "Email": $("#user").val(),
            "Password": $("#pass").val(),
            "ConfirmPassword": $("#pass").val()
        };

        $.ajax({
            type: "POST",
            url: `http://${host}/api/Account/Register`,
            data: sendData

        }).done(() => {
            $("#logForm")[0].reset();
            alert("Uspesna registracija na sistem!");

        }).fail(() => {
            alert("Greska prilikom registracije! Proverite unos!");
        });
    });

    $("#btnLog").click(() => {

        const sendData = {
            "grant_type": "password",
            "username": $("#user").val(),
            "password": $("#pass").val()
        };

        $.ajax({
            "type": "POST",
            "url": `http://${host}/Token`,
            "data": sendData

        }).done(data => {
            console.log(data);
            token = data.access_token;
            user = data.userName;
            $("#loggedName").empty().append(user);
            $("#logForm")[0].reset();
            $("#filterForm")[0].reset();
            onLogIn();
            loadZaposleni();
            headers.Authorization = 'Bearer ' + token;

        }).fail(() => {
            alert("Greska prilikom prijave!");
        });
    });

    $("#btnLogOut").click(() => {
        token = null;
        headers = {};

        onLogOut();
        loadZaposleni();
    });

    /*------------------------------------------------------------*/

    $("#btnShowReg").click(() => {
        $("#regDiv").hide();
        $("#formDiv").show();
        $("#bckDiv").show();
    });

    $("#btnPocetak").click(() => {
        $("#regDiv").show();
        $("#formDiv").hide();
        $("#bckDiv").hide();
    });

    function onLogIn() {
        $("#regDiv").hide();
        $("#formDiv").hide();
        $("#bckDiv").hide();
        $("#logOutDiv").show();
        $("#filterFormDiv").show();
    }

    function onLogOut() {
        $("#regDiv").show();
        $("#formDiv").hide();
        $("#bckDiv").hide();
        $("#logOutDiv").hide();
        $("#filterFormDiv").hide();
        $("#editFormDiv").hide();
    }
});