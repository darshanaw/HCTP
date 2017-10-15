$(function(){
	
	//$('#countdown').countup();
    var sinceYear = new Date('01/01/2005');
    $('#countdown').countdown({ since: sinceYear });
});
