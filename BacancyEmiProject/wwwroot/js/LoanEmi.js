$(document).ready(function () {
    $("#clsLoanAmount,#clsLoanInterest,#clsNoOfYear").blur(function () {
        var loanAmount = $("#clsLoanAmount").val();
        var loanInterest = $("#clsLoanInterest").val();
        var noOfYear = $("#clsNoOfYear").val();

        var noOfMonthlyInstallment = 1;
        noOfMonthlyInstallment = noOfYear * 12;
        var rateOfInterest = loanInterest / (12 * 100);
        var noOfMonthlyIst = (1 + rateOfInterest);
        var EMI = loanAmount * rateOfInterest * noOfMonthlyIst * noOfMonthlyInstallment / (noOfMonthlyIst * noOfMonthlyInstallment - 1);

        $('#clsMonthlyEmi').val(EMI);
        $('#clsRateOfInterest').val(rateOfInterest);
    });

    $(function () {
        $('#showGrid').click(function (event) {
            event.preventDefault();
            if ($("#clsLoanAmount").val() === '' || $("#clsLoanAmount").val() === 0) {
                toastr.error('Please enter numeric Loan Amount');
            }
            else if ($("#clsLoanInterest").val() === '' || $("#clsLoanInterest").val() === 0) {
                toastr.error('Please enter numeric Loan Interest');
            }
            else if ($("#clsNoOfYear").val() === '' || $("#clsNoOfYear").val() === 0) {
                toastr.error('Please enter numeric No of Amount');
            }
            else if ($("#clsMonthlyEmi").val() === '' || $("#clsMonthlyEmi").val() === 0) {
                toastr.error('Please enter numeric Monthly EMI');
            }
            else if ($("#clsRateOfInterest").val() === '' || $("#clsRateOfInterest").val() === 0) {
                toastr.error('Please enter numeric Rate of Interest');
            }
            else {
                var CriteriaInputByUser = {
                    LoanAmount: $("#clsLoanAmount").val(),
                    LoanInterest: $("#clsLoanInterest").val(),
                    NoOfYear: $("#clsNoOfYear").val(),
                    MonthlyEmi: $('#clsMonthlyEmi').val(),
                    RateOfInterest: $('#clsRateOfInterest').val()
                };
                $.ajax({
                    type: "POST",
                    url: "/Home/GetTransactionGrid",
                    data: JSON.stringify(CriteriaInputByUser),
                    dataType: "json",
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        var json = $.parseJSON(data);
                        var tableHtml = '';
                        $.each(json, function (index) {
                            tableHtml = tableHtml +
                                '<tr><td>' + data[index].Opening + '</td><td>' + data[index].Principal + '</td><td>' + data[index].Interest + '</td><td>' + data[index].Emi + '</td><td>' + data[index].Closing + '</td><td>' + data[index].CummulativeInterest + '</td></tr>';
                        });

                        $("#transactionBody").html(tableHtml);

                        $('#showGrid').show();
                    },
                    error: function () {
                        $('#showGrid').hide();
                    }
                });
            }
        });
    });
});