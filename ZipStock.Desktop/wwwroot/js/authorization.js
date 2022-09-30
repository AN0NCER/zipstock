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
        ipcRenderer.send("verification-email", $('.email').val()));
    } else {
        ipcRenderer.send("async-msg", "Bad Email");
    }
};

