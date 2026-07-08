const bigButton = document.getElementById("bigButton");
const mainEmoji = document.getElementById("mainEmoji");
const scene = document.getElementById("scene");
const question = document.getElementById("question");
const answers = document.getElementById("answers");
const reaction = document.getElementById("reaction");

const scenes = [
  ["🦖", "T-Rex slips on a banana peel while holding a lightsaber."],
  ["👽", "An alien rides a toilet rocket through Booger Town."],
  ["🐀", "Rick Ratman does the Cha Cha Slide inside Spirit Halloween."],
  ["🤖", "A spooky animatronic sneezes so hard its hat flies off."],
  ["💩", "King Poopy loses his crown in a monster truck."],
  ["🤢", "A giant booger chases a dinosaur through Home Depot."],
  ["🐶", "Sully gets toilet paper stuck to his shoe."],
  ["🦭", "Neil the Seal does the Cotton Eye Joe on the moon."],
  ["🎃", "A pumpkin monster slips into a pile of marshmallows."],
  ["🚜", "A monster truck drives over spaghetti and yells beep beep."]
];

const answerChoices = [
  ["😂", "That was funny!"],
  ["🍌", "Watch out!"],
  ["💃", "Dance!"],
  ["🤫", "Nothing"],
  ["🤢", "Boogers!"],
  ["⭐", "Use the Force!"],
  ["🌮", "More tacos!"],
  ["💩", "Poop crown!"]
];

function pick(list) {
  return list[Math.floor(Math.random() * list.length)];
}

function say(text) {
  if (!("speechSynthesis" in window)) return;

  speechSynthesis.cancel();

  const voice = new SpeechSynthesisUtterance(text);
  voice.rate = 0.85;
  voice.pitch = 1.1;

  speechSynthesis.speak(voice);
}

function sayQuestion() {
  say("Aaaaand... what would you say?");
}

function shake() {
  document.body.classList.remove("shake");
  void document.body.offsetWidth;
  document.body.classList.add("shake");
}

function makeAnswers() {
  const picked = [["🤫", "Nothing"]];

  while (picked.length < 4) {
    const next = pick(answerChoices);
    if (!picked.some(item => item[1] === next[1])) {
      picked.push(next);
    }
  }

  picked.sort(() => Math.random() - 0.5);

  answers.innerHTML = "";

  picked.forEach(item => {
    const button = document.createElement("button");
    button.className = "answer";
    button.innerHTML = `${item[0]}<br>${item[1]}`;
    button.onclick = () => answer(item[1]);
    answers.appendChild(button);
  });
}

function pressButton() {
  say("Pbbbbbbt!");
  shake();

  reaction.textContent = "";
  answers.innerHTML = "";
  question.textContent = "";
  mainEmoji.textContent = "🟠";
  scene.textContent = "Thinking...";

  setTimeout(() => {
    const chosen = pick(scenes);

    mainEmoji.textContent = chosen[0];
    scene.textContent = chosen[1];
    question.textContent = "...aaaand what would you say?";

    makeAnswers();

    setTimeout(sayQuestion, 5000);
  }, 700);
}

function answer(text) {
  shake();

  if (text === "Nothing") {
    say("Nothing");

    setTimeout(() => {
      say("You can't say nothing!");
      reaction.textContent = "😂 You can't say nothing!";
    }, 900);

  } else {
    say(text);
    reaction.textContent = "🎉 " + text;
  }
}

bigButton.onclick = pressButton;
question.onclick = sayQuestion;
