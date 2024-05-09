// some scripts

// jquery ready start
$(document).ready(function () {
    // jQuery code




    /* ///////////////////////////////////////

    THESE FOLLOWING SCRIPTS ONLY FOR BASIC USAGE, 
    For sliders, interactions and other

    */ ///////////////////////////////////////


    //////////////////////// Prevent closing from click inside dropdown
    $(document).on('click', '.dropdown-menu', function (e) {
        console.log('List has been clicked');
        e.stopPropagation();
    });

    $('.dropdown-toggle').dropdown();

    $(".link").click(function (event) {
        console.log('chamou');
        event.stopPropagation();
    });

    function showDropDown() {
        console.log('closeDropdown');
    }



    //////////////////////// Bootstrap tooltip
    if ($('[data-toggle="tooltip"]').length > 0) {  // check if element exists
        $('[data-toggle="tooltip"]').tooltip()
    } // end if





});
// jquery end

