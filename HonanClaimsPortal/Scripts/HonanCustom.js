function convertTime12to24(time12h) {

  const [time, modifier] = time12h.split(' ');

  let [hours, minutes] = time.split(':');

  if (hours === '12') {
    hours = '00';
  }

  if (modifier === 'PM') {
    hours = parseInt(hours, 10) + 12;
  }

  return hours + ':' + minutes;
}


function customGetHour(time12h)
{
    if (time12h == '')
        return "00";

    var res = convertTime12to24(time12h);
    var arr = res.split(':');

    if (arr[0] == '' || typeof arr[0] === 'undefined')
        return "00";

    return arr[0];
}

function customGetMinute(time12h) {

    if (time12h == '')
        return "00";

    var res = convertTime12to24(time12h);
    var arr = res.split(':');
   
    if (arr[1] == '' || typeof arr[1] === 'undefined')
        return "00";

    return arr[1];
}