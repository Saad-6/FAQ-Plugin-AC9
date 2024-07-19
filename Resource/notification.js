$(document).ready(function () {
    alert("Document loaded")
    function handleSuccess() {
    $('#askQuestionModal').modal('hide');
    toastr.success('Question submitted successfully');

    }

})