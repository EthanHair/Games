var blazorInterop = {};

blazorInterop.showAlert = function (message) {
    alert(message);
};

blazorInterop.logToConsoleTable = function (obj) {
    console.table(obj);
};

blazorInterop.showPrompt = function (message, defaultValue) {
    return prompt (message, defaultValue);
};

blazorInterop.createCard = function (rank, suit) {
    return { rank, suit };
};

blazorInterop.toggleHideElement = function (elementId) {
    document.getElementById(elementId).classList.toggle("d-none");
};