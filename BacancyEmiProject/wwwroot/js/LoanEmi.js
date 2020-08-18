$(document).ready(function () {

    $(".numeric").keydown(function (event) {
        // Allow only backspace and delete
        if (event.keyCode == 46 || event.keyCode == 8) {
            // let it happen, don't do anything
        }
        else {
            // Ensure that it is a number and stop the keypress
            if (event.keyCode < 48 || event.keyCode > 57) {
                event.preventDefault();
            }
        }
    });

    $("#clsLoanAmount,#clsLoanInterest,#clsNoOfYear").blur(function () {
        var loanAmount = $("#clsLoanAmount").val();
        var loanInterest = $("#clsLoanInterest").val();
        var noOfYear = $("#clsNoOfYear").val();

        var noOfMonthlyInstallment = 1;
        noOfMonthlyInstallment = noOfYear * 12;
        var rateOfInterest = loanInterest / (12 * 100);
        var noOfMonthlyIst = (1 + rateOfInterest);
        var EMI = loanAmount * rateOfInterest * noOfMonthlyIst * noOfMonthlyInstallment / (noOfMonthlyIst * noOfMonthlyInstallment - 1);

        $('#clsMonthlyEmi').val(EMI.toFixed(2));
        $('#clsRateOfInterest').val(rateOfInterest.toFixed(2));
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
                var criteriaInputByUser = {
                    LoanAmount: parseFloat($("#clsLoanAmount").val()),
                    LoanInterest: parseFloat($("#clsLoanInterest").val()),
                    NoOfYear: parseFloat($("#clsNoOfYear").val()),
                    MonthlyEmi: parseFloat($('#clsMonthlyEmi').val()),
                    RateOfInterest: parseFloat($('#clsRateOfInterest').val())
                };
                $.ajax({
                    type: "POST",
                    url: "/Home/GetTransactionGrid",
                    data: JSON.stringify(criteriaInputByUser),
                    dataType: "json",
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        var tableHtml = '';
                        $.each(data, function (index) {
                            tableHtml = tableHtml +
                                '<tr><td style="border: 1px solid black">' + data[index].opening + '</td><td style="border: 1px solid black">' + data[index].principal + '</td><td style="border: 1px solid black">' + data[index].interest + '</td><td style="border: 1px solid black">' + data[index].emi + '</td><td style="border: 1px solid black">' + data[index].closing + '</td><td style="border: 1px solid black">' + data[index].cummulativeInterest + '</td></tr>';
                        });

                        $("#transactionBody").html(tableHtml);
                        $(".clsGrid").removeAttr('hidden');
                        $('#showGrid').show();
                    },
                    error: function () {
                        //$('#showGrid').hide();
                    }
                });
            }
        });
    });
});