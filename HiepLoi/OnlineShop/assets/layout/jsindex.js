$(document).ready(function () {
	processMessage();

	//thong bao sau khi add-edit-delete
	function processMessage() {
		var msgSuccess = $('#msgSuccess').val();
		var msgError = $('#msgError').val();
		if (msgSuccess) {
			new PNotify({
				title: ' Thành công',
				text: msgSuccess,
				type: 'success',
				styling: 'bootstrap3'
			});
		}
		if (msgError) {
			new PNotify({
				title: ' Không thành công',
				text: msgError,
				type: 'error',
				styling: 'bootstrap3'
			});
        }
        var url = $('#URL_APPLICATION').val() + "/Home/XoaPNotifySession";
        $.get(url, function (data, status) {
            //alert("Thành công");
        });
	}
});
//check cookie to show or hide search pannel
if ($.cookie('CookieSiteSearch') == 1) {
	$('#pnlSearch').show();
}
else {
	$('#pnlSearch').hide();
}
$('#B1').click(function (event) {
	//set cookie
	if ($.cookie('CookieSiteSearch') == 1) {
		$.cookie('CookieSiteSearch', 0);
	}
	else {
		$.cookie('CookieSiteSearch', 1);
	}
	$('#pnlSearch').slideToggle(500);
});