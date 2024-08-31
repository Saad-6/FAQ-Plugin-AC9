function handleSuccess() {
    console.log("Entered handlesuccess")
    document.getElementById('question').value = '';
    $('#question').val('');
    $('#askQuestionModal').modal('hide');
    const successMessage = document.getElementById('successMessage');
    successMessage.style.display = 'block';
    successMessage.scrollIntoView({ behavior: 'smooth', block: 'start' });
    setTimeout(function () {
        successMessage.style.display = 'none';
    }, 10000);
}