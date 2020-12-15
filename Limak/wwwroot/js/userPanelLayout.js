$(function () {
    var activePage = $("#userPanelNavBar").attr("data-acitve-page");
    
     $("#userPanelNavBar").find(`li[data-page="${activePage}"]`).addClass("active");
})