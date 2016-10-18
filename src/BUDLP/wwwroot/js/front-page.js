$(function () {

    // Init all dropdowns to Semantic UI dropdown
    $('.ui.dropdown')
        .dropdown()
    ;

    // Front-page login button toggle login modal
    $('.login-modal')
        .modal('attach events', '.login-button')
    ;

    // Front-page topic toggle all
    $('.master.topic.checkbox')
        .checkbox({
      // check all children
      onChecked: function () {
          var
            $childCheckbox = $('.table .checkbox');
          ;
          $childCheckbox.checkbox('check');
      },
      // uncheck all children
      onUnchecked: function () {
          var
            $childCheckbox = $('.table .checkbox');
          ;
          $childCheckbox.checkbox('uncheck');
      }
  })
    ;
});

