(() => {
    function initCalendar() {
        const el = document.getElementById('calendar');
        if (!el || typeof FullCalendar === 'undefined') {
            return;
        }
        const employeeId = el.getAttribute('data-employee');
        const calendar = new FullCalendar.Calendar(el, {
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
    }

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', initCalendar);
    } else {
        initCalendar();
    }
})();