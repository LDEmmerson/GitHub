// to reference my html ids im creating a constant for them

const billInput = document.getElementById("enteredBill");
const tipCheckbox = document.getElementById("leaveTip");
const tipAmounts = document.getElementById("tipAmounts");
const tipSelect = document.getElementById("tipPercentage");
const customTip = document.getElementById("customTip");
const calculateButton = document.getElementById("calculateButton")
const results = document.getElementById("Result")

// putting the checkbox options in with a check box
tipCheckbox.addEventListener("change", () => {
    tipAmounts.style.display = tipCheckbox.checked ? "block" : "none";
});
//  used to find the most appropriate event (https://www.w3schools.com/jsref/dom_obj_event.asp)
// showing the custom option wehen the box is ticked
tipSelect.addEventListener("change", () => {
    customTip.style.display = tipSelect.value === "custom" ? "block" : "none";
});

// adding the calculate button into the html

calculateButton.addEventListener("click", () => { // looking for the event "click" to happen to my button
    const bill = parseFloat(billInput.value); // converting the inputed string to a number 
    if (isNaN(bill) || bill <= 0) {  // asking the script to check "if bill is not a number" and/or equal to or less than 0 then say "error"
        results.textContent = "Please enter a valid number.";
        return;
}

let tipPercentage = 0; // default amount is set to 0
if (tipCheckbox.checked) {
        tipPercentage = tipSelect.value === "custom"
            ? parseFloat(customTip.value)
            : parseFloat(tipSelect.value);

    if(isNaN(tipPercentage) || tipPercentage < 0){ // also saying that If the tip percentage is not a number or 0
        results.textContent = "Please enter a valid number.";
        return;
    }
}

let tip = (bill * tipPercentage / 100).toFixed(2);
let total = (bill + parseFloat(tip)).toFixed(2);

results.textContent = `Tip: £${tip} | Total: £${total}`;
});