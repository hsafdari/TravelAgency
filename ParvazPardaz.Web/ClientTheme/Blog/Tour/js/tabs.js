tabBars = document.querySelectorAll(".tab-bar a")
contents = document.querySelectorAll(".content")

resetTabs()
contents[0].style = `display: block;`
// tabBars[0].style = `background-color: #26336a; color: #fff`

tabBars.forEach(function(tabBar, tabBarIndex) {
  tabBar.addEventListener("click", function() {
    resetTabs()
    contents[tabBarIndex].style = `display: block; `
    // this.style = `background-color: #26336a;`
  })
})

function resetTabs() {
  for (let i = 0; i < contents.length; i++) {
    contents[i].style = `display: none;`
    // tabBars[i].style = `background-color: #21a192; color: #fff;`
  }
}

function checkTab(e) {
    if (e.keyCode === 9) {
      for (let i = 0; i < tabBars.length; i++) {
        tabBars[i].classList.add('show-outline')
      }
      window.removeEventListener('keydown', checkTab);
    }
}
window.addEventListener('keydown', checkTab);