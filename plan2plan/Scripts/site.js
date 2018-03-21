function isEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
}


/**  for feedback ****/
function onSuccessFeedback() {
    $("#feedback_form").fadeIn(1000).hide("slow");
    $("#feedback_answer").next().show();
}
function onFailureFeedback() {
    $("#results_feedback").html("При сохранении обращения произошла ошибка. Попробуйте позже.");
}

function reSendMessage() {
    $("#feedback_form").show("slow");
    $("#feedback_answer").html("");
    $("#feedback_answer").next().hide();
}
/**  for feedback ****/