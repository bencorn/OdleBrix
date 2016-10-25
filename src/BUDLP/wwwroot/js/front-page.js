$(function () {



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

