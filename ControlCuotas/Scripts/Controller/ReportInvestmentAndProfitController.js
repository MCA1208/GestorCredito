var param = null;


function SearchReportGanancia() {

    $.blockUI();

    param = {
        DateStart: $('#dateFrom').val(),
        DateEnd: $('#dateUp').val()
    };

 
    $.post(directories.report.GetReportGanancia, param)
        .done(function (data) {
            if (data.status !== "error") {

                $('#tblReportGanancia > tbody').html('');
                var _html = '';
                _html += '<tbody class="customtable" style= text-align:left;>';
                data = JSON.parse(data.result);
                $.each(data, function (key, value) {
                    
                    _html += '<tr><td>' + value.dateFrom + '</td><td>' + value.dateUp + '</td><td >' + value.Inversion + '</td><td >' + value.SumaIntereses + '</td><td >' + value.GananciaReal + '</td>';

                });

                _html += '</tbody >';

                $('#tblReportGanancia').append(_html);

                $('#tblReportGanancia').DataTable({
                    destroy: true,
                    retrieve: true,
                    dom: 'Bfrtip',
                    buttons: [
                        { "extend": 'excel', "text": '<span data-toggle="tooltip" data-placement="top" title="Exportar Excel" class="fas fa-file-excel fa-2x"></span>' },
                        { "extend": 'pdf', "text": '<span data-toggle="tooltip" data-placement="top" title="Exportar PDF" class="fas fa-file-pdf fa-2x" ></span>' },
                        { "extend": 'print', "text": '<span data-toggle="tooltip" data-placement="top" title="Imprimir" class="fas fa-print fa-2x"></span>' }
                    ]

                });

            }
            else {
                alertify.error(data.message);

            }

        })
        .fail(function (data) {
            alertify.error(data.statusText);
        })
        .always(function () {
            $.unblockUI();
        });

}

function formatDate(date) {
    //var d = new Date(date),
        month = '' + date.getMonth(),
        day = '' + date.getDate(),
        year = date.getFullYear();

    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;

    return [day, month, year].join('-');
}