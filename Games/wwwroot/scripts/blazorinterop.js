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

// JavaScript function to call the Dice Roller API
blazorInterop.rollDice = function (numDice, numSides, numRolls) {
    const rollRequest = {
        numberDice: numDice,
        numberSides: numSides,
        numberRolls: numRolls
    };

    fetch("https://localhost:7053/roll", {
        method: "POST",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(rollRequest)
    })
        .then(response => response.json())
        .catch(error => console.error(error))
}