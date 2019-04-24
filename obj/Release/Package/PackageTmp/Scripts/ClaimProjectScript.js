$.datepicker.regional['th'] = {
    changeMonth: true,
    changeYear: true,
    //defaultDate: GetFxupdateDate(FxRateDateAndUpdate.d[0].Day),
    yearOffSet: 543,
    buttonImageOnly: true,
    dateFormat: 'dd-mm-yy',
    dayNames: ['อาทิตย์', 'จันทร์', 'อังคาร', 'พุธ', 'พฤหัสบดี', 'ศุกร์', 'เสาร์'],
    dayNamesMin: ['อา', 'จ', 'อ', 'พ', 'พฤ', 'ศ', 'ส'],
    monthNames: ['มกราคม', 'กุมภาพันธ์', 'มีนาคม', 'เมษายน', 'พฤษภาคม', 'มิถุนายน', 'กรกฎาคม', 'สิงหาคม', 'กันยายน', 'ตุลาคม', 'พฤศจิกายน', 'ธันวาคม'],
    monthNamesShort: ['ม.ค.', 'ก.พ.', 'มี.ค.', 'เม.ย.', 'พ.ค.', 'มิ.ย.', 'ก.ค.', 'ส.ค.', 'ก.ย.', 'ต.ค.', 'พ.ย.', 'ธ.ค.'],
    constrainInput: true,

    prevText: 'ก่อนหน้า',
    nextText: 'ถัดไป',
    yearRange: '-5:+0',
    buttonText: 'เลือก',

};
$.datepicker.setDefaults($.datepicker.regional['th']);

function getAge(oject) {
    var dayBirth = oject;
    var getdayBirth = dayBirth.split("-");
    var YB = getdayBirth[2] - 543;
    var MB = getdayBirth[1];
    var DB = getdayBirth[0];

    var setdayBirth = moment(YB + "-" + MB + "-" + DB);
    var setNowDate = moment();
    var yearData = setNowDate.diff(setdayBirth, 'years', true); // ข้อมูลปีแบบทศนิยม  
    var yearFinal = Math.round(setNowDate.diff(setdayBirth, 'years', true), 0); // ปีเต็ม  
    var yearReal = setNowDate.diff(setdayBirth, 'years'); // ปีจริง  
    var monthDiff = Math.floor((yearData - yearReal) * 12); // เดือน  
    var str_year_month = yearReal + " ปี " + monthDiff + " เดือน"; // ต่อวันเดือนปี
    return str_year_month;
}

function getDateDiff(strDate1, strDate2) {
    var date1 = strDate1;
    var date2 = strDate2;
    date1 = date1.split("-");
    date2 = date2.split("-");
    sDate = new Date((date1[2]-543), date1[1] - 1, date1[0]);
    eDate = new Date((date2[2] - 543), date2[1] - 1, date2[0]);

    // Copy date objects so don't modify originals
    var s = sDate
    var e = eDate

    // Set time to midday to avoid dalight saving and browser quirks
    s.setHours(12, 0, 0, 0);
    e.setHours(12, 0, 0, 0);

    // Get the difference in whole days
    var totalDays = Math.round((e - s) / 8.64e7);

    // Get the difference in whole weeks
    var wholeWeeks = totalDays / 7 | 0;

    // Estimate business days as number of whole weeks * 5
    var days = wholeWeeks * 5+1;

    // If not even number of weeks, calc remaining weekend days
    if (totalDays % 7) {
        s.setDate(s.getDate() + wholeWeeks * 7);
        
        while (s < e) {
            s.setDate(s.getDate()+1);

            // If day isn't a Sunday or Saturday, add to business days
            //if (s.getDay() != 0 && s.getDay() != 6) {
                ++days;
            //}
        }
    }
    return days;
}

function confirmDelete(sender) {
    if ($(sender).attr("confirmed") == "true") { return true; }

    bootbox.confirm({
        size: "small",
        message: "คุณต้องการลบข้อมูล ใช่หรือไม่",
        buttons: {
            confirm: {
                label: 'ใช่',
                className: 'btn btn-success'
            },
            cancel: {
                label: 'ไม่ใช่',
                className: 'btn btn-danger'
            }
        }
        , callback: function (confirmed) {
            if (confirmed) {
                $(sender).attr('confirmed', confirmed);
                sender.click();
            }
        }
    });

    return false;
}

function confirmAddOldEmp(sender) {
    if ($(sender).attr("confirmed") == "true") { return true; }
    bootbox.confirm({
        size: "small",
        message: "คุณต้องเพิ่มพนักงาน ใช่หรือไม่",
        buttons: {
            confirm: {
                label: 'ใช่',
                className: 'btn btn-success'
            },
            cancel: {
                label: 'ไม่ใช่',
                className: 'btn btn-danger'
            }
        }
        , callback: function (confirmed) {
            if (confirmed) {
                $(sender).attr('confirmed', confirmed);
                sender.click();
            }
        }
    });

    return false;
}


