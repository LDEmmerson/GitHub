// Builing the DOM Elements 
const gameWindow = document.querySelector(".Container");
const playerResults = document.querySelector(".Player-Results img");
const AiResults = document.querySelector(".Ai-Results img");
const Result = document.querySelector(".Result");
const imageOptions = document.querySelectorAll(".Img_Option");
const PlayerScore = document.getElementById("PlayerScore");
const AiScore = document.getElementById("AiScore");
const resetButton = document.querySelector(".ResetButton");
// Logging to confirm all selected correctly
console.log(gameWindow, playerResults, AiResults, Result, imageOptions);

// Creating a simple score tracker
let playerPoints = 0;
let aiPoints = 0;

// Looping through my imageOptions Image elements 
imageOptions.forEach((image, index) => {
    // Adding an event listener 
    image.addEventListener("click", (clicked) => {
        image.classList.add("active");
        playerResults.src = AiResults.src = "Media/rock.jpg"; //Sets both defaults to rock
        Result.textContent ="AI thinking..."
        // If there is a click add the class "active" to it and now loop back through again looking for "active"
        imageOptions.forEach((image2, index2) => {
            // if when checking the image dosent match the index it removes the active class tag from the others
            index !== index2 && image2.classList.remove("active");
    });
    //adding in a check
    gameWindow.classList.add("GameStarted")
    // added a timer to delay my results 
    let TimeDelay = setTimeout(() => { // Making a timer function using an arrow function
                gameWindow.classList.remove("GameStarted")
            const imageSource = image.querySelector("img").src;
            // console.log(imageSource); //console logged to check
            playerResults.src = imageSource; // display the users selected image 
            // Allowing the Ai to make a random choice by picking a number between 0, 1, and 2
            let randomNumber = Math.floor(Math.random() * 3);
            //  creating the Ai options 
            let aiImages = ["Media/rock.jpg",
                            "Media/paper.png",
                            "Media/scissors.png"]; 
            //Struggled alot to get the images to update, i was adding /media/rock.jpg - "/" tells the script to start at the root, not the project folder
            AiResults.src = aiImages[randomNumber];
            //Giving the play and the AI a letter value, "R" for rock "S" for scissors and "P" for paper
            let aiLetter = ["R", "P","S"][randomNumber] // using a random number generated above to pick the letter
            let playerLetter = ["R", "P","S"][index] // using the chosen image assign the letter
            // Using this later to determin the winner
            // Logged the results to console to check them
            // console.log(playerLetter,aiLetter)
            // making my outcomes
            let Outcomes = {
                RR: "Draw",
                RS: "You win!",
                RP: "AI wins!",
                PR: "You win!",
                PS: "AI wins!",
                PP: "Draw",
                SR: "AI wins!",
                SS: "Draw",
                SP: "You win!",
            };
            //Basing the outcome on what the user and Ai options are 
            const OutcomeResult = Outcomes[playerLetter + aiLetter];
            // console.log(OutcomeResult); // Logged my result to check its working
            // Now displaying the outcome of the game 
            Result.textContent = OutcomeResult;
            // If both options match, Draw

            // Trying to display the score
            if (OutcomeResult === "You win!") {
                playerPoints++;
                Result.style.color = "Green";
            } else if (OutcomeResult === "AI wins!") {
            aiPoints++;
            Result.style.color = "red";
            } else {
                Result.style.color = "black";
            }

            // Display the updated score and colors 
            PlayerScore.textContent = playerPoints;
            AiScore.textContent = aiPoints;
    }, 2000);
    });
});

//Reset button
resetButton.addEventListener("click", () => { // Must be lower case for "click"
    playerPoints = 0;
    aiPoints = 0;
    PlayerScore.textContent = "0";
    AiScore.textContent = "0";
    Result.textContent = " You have reset the scores!";
    Result.style.color = "black";
});