

var currentStep = 1;
var updateProgressBar;

updateProgressBar = function () {
   
    var progressPercentage = 20 * (currentStep-1);
    $(".progress-bar").css("width", progressPercentage + "%");
}

export function displayStep(stepNumber) {
   
    //hide other steps div
    $('#multi-step-form').find('.step').hide();
    //show current step 
    $(".step-" + stepNumber).show();
    currentStep = stepNumber;
    updateProgressBar();
   
}

export function navigate() {

    //hide other steps div
    $('#multi-step-form').find('.step').hide();
    //show current step 
    $(".step-" + currentStep).show();
    updateProgressBar();

}


$(document).ready(function () {

    //hide navbar on page load
    document.getElementById('sidebar').classList.toggle('auto-expanded')
    document.getElementById('menu-top').classList.toggle('hidden')

    //hide other steps div
    //$('#multi-step-form').find('.step').slice(1).hide();

    $(".prev-step").click(function () {
        if (currentStep > 1) {
            $(".step-" + currentStep).addClass("animate__animated animate__fadeOutRight");
            currentStep--;
            setTimeout(function () {
                $(".step").removeClass("animate__animated animate__fadeOutRight").hide();
                $(".step-" + currentStep).show().addClass("animate__animated animate__fadeInLeft");
                //move to target page
                navigate();
            }, 500);
        }
    });

});


