document.addEventListener("DOMContentLoaded", function () {  
   const sidebarToggles = document.querySelectorAll("#sidebar .nav-link[data-bs-toggle='collapse']");  
   sidebarToggles.forEach(toggle => {  
       toggle.addEventListener("click", function () {  
           const target = document.querySelector(this.getAttribute("href"));  
           if (target) {  
               target.classList.toggle("show");  
           }  
       });  
   });  
});
