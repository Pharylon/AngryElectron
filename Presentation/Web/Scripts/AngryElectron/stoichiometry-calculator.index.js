/// <reference path="jquery-1.9.1.js" />

$(function () {
	$(".tagManager").tagsManager();
	$(".anotherTagManager").tagsManager();
	$("#btn-balance-equation").on("click", function () {
		var formData = { unbalancedEquation: $("#unbalanced-equation").val() };
		$.post($("#single-field-entry").attr("action"), formData, function (data) {
			$("#balanced-equation-result").html(data);
		});
	});
});
