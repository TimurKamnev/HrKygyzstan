// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const scrollLabel1 = document.querySelector('.scroll-label1');
const scrollLabel2 = document.querySelector('.scroll-label2');
const scrollLabel3 = document.querySelector('.scroll-label3');
const button1 = document.querySelector('#button1');
const button2 = document.querySelector('#button2');
const button3 = document.querySelector('#button3');

scrollLabel1.addEventListener('click', () => {
  button1.scrollIntoView({behavior: 'smooth'});
})
scrollLabel2.addEventListener('click', () => {
  button2.scrollIntoView({behavior: 'smooth'});
})

scrollLabel3.addEventListener('click', () => {
  button3.scrollIntoView({behavior: 'smooth'});
})


/* let availableKeywords = [
  'HTML',
  'CSS',
  'EasyTutorials',
  'Web design tutorials',
  'JavaScript',
  'Where to learn coding online',
  'Where to learn web coding',
  'How to create website'
];

const resultsBox = document.querySelector(".result-box");
const inputBox = document.getElementById("input-box");

inputBox.onkeyup =  function(){
  let result = [];
  let input = inputBox.value;
  if(input.length){
    result = availableKeywords.filter((keyword) => {
      return keyword.toLowerCase().includes(input.toLowerCase());
    });
    console.log(result);
  }
  display(result);
  
  if(!result.length){
    resultsBox.innerHTML = '';
  }
}

function display(result){
  const content = result.map((list) => {
    return "<li onclick=selectInput(this)>" + list + "</li>";
  });

  resultsBox.innerHTML = "<ul>" + content.join('') + "</ul>";
}

function selectInput(list){
  inputBox.value = list.innerHTML;
  resultsBox.innerHTML = '';
} */