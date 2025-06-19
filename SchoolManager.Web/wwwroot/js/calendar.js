document.addEventListener('DOMContentLoaded', function () {
    var el = document.getElementById('calendar');
    if (!el || typeof FullCalendar === 'undefined') {
        return;
    }
    var employeeId = el.getAttribute('data-employee');
    var calendar = new FullCalendar.Calendar(el, {
        initialView: 'dayGridMonth',
        events: function (info, successCallback, failureCallback) {
            $.ajax({
                url: '/Schedule/GetEvents',
                data: {
                    employeeId: employeeId,
                    start: info.startStr,
                    end: info.endStr
                }
            }).done(successCallback).fail(failureCallback);
        }
    });
    calendar.render();
});