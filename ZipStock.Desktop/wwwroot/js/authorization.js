const { ipcRenderer } = require("electron");

function BtnClicked() {
    const validateEmail = (email) => {
        return String(email)
            .toLowerCase()
            .match(
                /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
            );
    };
    if (validateEmail($('.email').val())) {
        ipcRenderer.send("verification-email", $('.email').val());
    } else {
        ipcRenderer.send("async-msg", "Bad Email");
    }
};

function CheckCode() {
    let a, b, c, d;
    a = $('.code_1').val();
    b = $('.code_2').val();
    c = $('.code_3').val();
    d = $('.code_4').val();
    if (a.length > 0 || b.length > 0 || c.length > 0 || d.length > 0) {
        let code = `${a.toUpperCase()}${b.toUpperCase()}${c.toUpperCase()}${d.toUpperCase()}`;
        ipcRenderer.send("verification-code", code);
    }
}


ipcRenderer.on('code-verify', async (event, args) => {
    let active = $("main.active");
    let disable = $("main:not(.active)");
    active.removeClass('active');
    await sleep(1000);
    disable.addClass('active');
});

ipcRenderer.on('loading-code', async (event, args) => {
    let form = $(".code_verify");
    let loading = $(".code_loading");
    if (form.hasClass("show")) {
        form.removeClass("show");
        await sleep(100);
        loading.addClass("show");
    } else {
        loading.removeClass("show");
        await sleep(100);
        form.addClass("show");
    }
});

ipcRenderer.on('logged-account', async (event, args) => {
    window.location.href = "/";
});


var container = document.getElementsByClassName("code_input")[0];
container.onkeyup = function (e) {
    var target = e.srcElement || e.target;
    var maxLength = parseInt(target.attributes["maxlength"].value, 10);
    var myLength = target.value.length;
    if (myLength >= maxLength) {
        var next = target;
        while (next = next.nextElementSibling) {
            if (next == null)
                break;
            if (next.tagName.toLowerCase() === "input") {
                next.focus();
                break;
            }
        }
    }
    // Move to previous field if empty (user pressed backspace)
    else if (myLength === 0) {
        var previous = target;
        while (previous = previous.previousElementSibling) {
            if (previous == null)
                break;
            if (previous.tagName.toLowerCase() === "input") {
                previous.focus();
                break;
            }
        }
    }
}


function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}