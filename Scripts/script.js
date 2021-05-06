$(document).ready(function () {
    /* prepare tables */

    $.extend($.fn.dataTable.defaults, {
        info: false,
        searching: false,
        paging: false
    });

    $('#balance').DataTable();
    $('#rates').DataTable();

    $('#transactions').DataTable({
        language: {
            "sInfo": "Showing _START_ to _END_ of _TOTAL_ entries",
            "oPaginate": {
                "sNext": ">",
                "sPrevious": "<"
            }
        },
        paging: true,
        info: true,
        dom: 'tip',
        order: [[0, "desc"]],
        autoWidth: false
    });

    $('#orders').DataTable({
        language: {
            "sInfo": "Showing _START_ to _END_ of _TOTAL_ entries",
            "oPaginate": {
                "sNext": ">",
                "sPrevious": "<"
            }
        },
        paging: true,
        info: true,
        dom: 'tip',
        order: [[0, "desc"]],
        autoWidth: false
    });

    /* fix margins when footer changes height */

    setMarginBottom();
    $(window).resize(function () { setMarginBottom(); });

    /* get and format date and time */

    const monthNames = ["January", "Februrary", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];

    window.setInterval(function () {
        var d = new Date();
        var dtime = monthNames[d.getMonth()] + " " +
            d.getDate() + ", " +
            d.getFullYear() + " " +
            (d.getHours() < 10 ? "0" : "") + d.getHours() + ":" +
            (d.getMinutes() < 10 ? "0" : "") + d.getMinutes() + ":" +
            (d.getSeconds() < 10 ? "0" : "") + d.getSeconds();
        $("#datetime").text(dtime);
    }, 1000);

    /* draw chart */

    if (document.getElementById('monthlyHighs') !== null) {
        var ctx = document.getElementById('monthlyHighs').getContext('2d');
        var monthlyHighs = new Chart(ctx, {
            type: 'line',
            data: {
                labels: ['Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec', 'Jan', 'Feb', 'Mar'],
                datasets: [{
                    label: 'max',
                    data: randomDataSet(9, 3000, 4000),
                    borderWidth: 1
                }]
            },
            options: {
                legend: {
                    display: false
                },
                elements: {
                    line: {
                        backgroundColor: 'rgba(149, 188, 242, 0.5)',
                        borderColor: 'rgba(8, 83, 148, 1)'
                    }
                }
            }
        });
    }
});

function setMarginBottom() {
    var baseMargin = 40;
    var newMarginBottom = parseInt($("footer").css("height")) + baseMargin;
    $("#main-content").css("margin-bottom", newMarginBottom + "px");
}

function randomDataSet(dataSetSize, minValue, maxValue) {
    return new Array(dataSetSize).fill(0).map(function (n) {
        return Math.random() * (maxValue - minValue) + minValue;
    });
}