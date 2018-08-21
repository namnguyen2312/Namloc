$(document).ready(function() {
 var mySwiper = new Swiper ('.main-slider', {
    // Optional parameters
    loop: true,
    // If we need pagination
    autoplay: {
        delay: 10000,
        disableOnInteraction: false,
      },
    pagination: {
      el: '.swiper-pagination',
      clickable: true,
    },

    // Navigation arrows
    // navigation: {
    //   nextEl: '.swiper-button-next',
    //   prevEl: '.swiper-button-prev',
    // }
  });
  var mySwiper1 = new Swiper ('.swiper-container', {
    slidesPerView: 7,
    // spaceBetween: 30,
    slidesPerGroup: 1,
    loop: true,
    loopFillGroupWithBlank: true,
    // pagination: {
    //   el: '.swiper-pagination',
    //   clickable: true,
    // },
    autoplay: {
        delay: 10000,
        disableOnInteraction: false,
      },
    navigation: {
      nextEl: '.swiper-button-next',
      prevEl: '.swiper-button-prev',
    },
  });

  //mobile-menu
  $("#my-menu").mmenu();
  var API = $("#my-menu").data( "mmenu" );
  $(".mobile-menu .text-left #primary-menu").click(function() {
    API.open();
  });

  //pagination: change active class
  $(".pagination1 li").click(function() {
    $('.pagination1 li.active').removeClass('active');
    $(this).addClass('active');
  });

  $("#primary-menu li").click(function (){
    $("#primary-menu li.active").removeClass('active');
    $(this).addClass('active');
  });
  //search button
  var isOpen = false;
  $(".icon-search-menu .wicon").click(function(){
      // $("div.hide-search").removeClass('hide-search').addClass('unhide-search');
    if(isOpen == false){
      $("#searchBox").removeClass('hide-search').addClass('unhide-search');
      // inputBox.focus();
      isOpen = true;
    } else {
      $("#searchBox").removeClass('unhide-search').addClass('hide-search');
      // inputBox.focusout();
      isOpen = false;
    }
  });
  $(document).click(function (e)
  {
    var container = $(".icon-search-menu .wicon")
    if (!container.is(e.target) && container.has(e.target).length === 0)
    {
      // $('#searchbox').hide();
      $("#searchBox").removeClass('unhide-search').addClass('hide-search');
    }
  });

  var isOpen = false;
  $(".icon-search-menu .wicon1").click(function(){
      // $("div.hide-search").removeClass('hide-search').addClass('unhide-search');
    if(isOpen == false){
      $("#searchBox1").removeClass('hide-search').addClass('unhide-search');
      // inputBox.focus();
      isOpen = true;
    } else {
      $("#searchBox1").removeClass('unhide-search').addClass('hide-search');
      // inputBox.focusout();
      isOpen = false;
    }
  });
  $(document).click(function (e)
  {
    var container = $(".icon-search-menu .wicon1")
    if (!container.is(e.target) && container.has(e.target).length === 0)
    {
      // $('#searchbox').hide();
      $("#searchBox1").removeClass('unhide-search').addClass('hide-search');
    }
  });


  //update date tour
  // $('#datetimepicker4').datetimepicker({
  //     format: 'DD/MM/YYYY'
  //   });
  // $('#datetimepicker5').datetimepicker({
  //     format: 'DD/MM/YYYY'
  //   });

  //accordion menu
  var accordionsMenu = $('.cd-accordion-menu');

  if( accordionsMenu.length > 0 ) {

    accordionsMenu.each(function(){
      var accordion = $(this);
      //detect change in the input[type="checkbox"] value
      accordion.on('change', 'input[type="checkbox"]', function(){
        var checkbox = $(this);
        console.log(checkbox.prop('checked'));
        ( checkbox.prop('checked') ) ? checkbox.siblings('ul').attr('style', 'display:none;').slideDown(300) : checkbox.siblings('ul').attr('style', 'display:block;').slideUp(300);
      });
    });
  }

  var accordionsMenu1 = $('.cd-accordion-menu1');

  if( accordionsMenu1.length > 0 ) {

    accordionsMenu1.each(function(){
      var accordion1 = $(this);
      //detect change in the input[type="checkbox"] value
      accordion1.on('change', 'input[type="checkbox"]', function(){
        var checkbox1 = $(this);
        console.log(checkbox1.prop('checked'));
        ( checkbox1.prop('checked') ) ? checkbox1.siblings('ul').attr('style', 'display:none;').slideDown(300) : checkbox1.siblings('ul').attr('style', 'display:block;').slideUp(300);
      });
    });
  }

  //

  //validation

  $("#customerForm").validate({
      rules: {
        fullname: {
          required: true,
          minlength: 6
        },
        email: {
          required: true,
          email: true
        },
        phone: {
          required: true,
          minlength: 6
        }
      },
      messages: {
        username: {
          required: "Vui lòng nhập họ tên",
          minlength: "vui lòng nhập họ tên đầy đủ"
        },
        phone: {
          required: "Vui lòng nhập số điện thoai",
          minlength: "Vui lòng nhập chính xác số điện thoại"
        },
        email: "Vui lòng nhập chính xác email"
      }
    });


      /* ===== Logic for creating fake Select Boxes ===== */
    $('.sel').each(function() {
      $(this).children('select').css('display', 'none');

      var $current = $(this);

      $(this).find('option').each(function(i) {
        if (i == 0) {
          $current.prepend($('<div>', {
            class: $current.attr('class').replace(/sel/g, 'sel__box')
          }));

          var placeholder = $(this).text();
          $current.prepend($('<span>', {
            class: $current.attr('class').replace(/sel/g, 'sel__placeholder'),
            text: placeholder,
            'data-placeholder': placeholder
          }));

          return;
        }

        $current.children('div').append($('<span>', {
          class: $current.attr('class').replace(/sel/g, 'sel__box__options'),
          text: $(this).text()
        }));
      });
    });

    // Toggling the `.active` state on the `.sel`.
    $('.sel').click(function() {
      $(this).toggleClass('active');
    });

    // Toggling the `.selected` state on the options.
    $('.sel__box__options').click(function() {
      var txt = $(this).text();
      var index = $(this).index();

      $(this).siblings('.sel__box__options').removeClass('selected');
      $(this).addClass('selected');

      var $currentSel = $(this).closest('.sel');
      $currentSel.children('.sel__placeholder').text(txt);
      $currentSel.children('select').prop('selectedIndex', index + 1);
    });

  //   function jssor_1_slider_init() {

  //           var jssor_1_SlideshowTransitions = [
  //             {$Duration:1200,$Opacity:2}
  //           ];

  //           var jssor_1_options = {
  //             $AutoPlay: 1,
  //             $SlideshowOptions: {
  //               $Class: $JssorSlideshowRunner$,
  //               $Transitions: jssor_1_SlideshowTransitions,
  //               $TransitionsOrder: 1
  //             },
  //             $ArrowNavigatorOptions: {
  //               $Class: $JssorArrowNavigator$
  //             },
  //             $BulletNavigatorOptions: {
  //               $Class: $JssorBulletNavigator$
  //             }
  //           };

  //           var jssor_1_slider = new $JssorSlider$("jssor_1", jssor_1_options);

  //           /*#region responsive code begin*/

  //           var MAX_WIDTH = 3000;

  //           function ScaleSlider() {
  //               var containerElement = jssor_1_slider.$Elmt.parentNode;
  //               var containerWidth = containerElement.clientWidth;

  //               if (containerWidth) {

  //                   var expectedWidth = Math.min(MAX_WIDTH || containerWidth, containerWidth);

  //                   jssor_1_slider.$ScaleWidth(expectedWidth);
  //               }
  //               else {
  //                   window.setTimeout(ScaleSlider, 30);
  //               }
  //           }

  //           ScaleSlider();

  //           $Jssor$.$AddEvent(window, "load", ScaleSlider);
  //           $Jssor$.$AddEvent(window, "resize", ScaleSlider);
  //           $Jssor$.$AddEvent(window, "orientationchange", ScaleSlider);
  //           /*#endregion responsive code end*/
  //       };
  // jssor_1_slider_init();
 // function jssor_1_slider_init2() {
 //      var jssor_1_SlideshowTransitions = [
 //        {$Duration:1200,$Opacity:2}
 //      ];

 //      var jssor_1_options = {
 //        $AutoPlay: 1,
 //        $SlideshowOptions: {
 //          $Class: $JssorSlideshowRunner$,
 //          $Transitions: jssor_1_SlideshowTransitions,
 //          $TransitionsOrder: 1
 //        },
 //        $ArrowNavigatorOptions: {
 //          $Class: $JssorArrowNavigator$
 //        },
 //        $BulletNavigatorOptions: {
 //          $Class: $JssorBulletNavigator$
 //        }
 //      };

 //      var jssor_1_slider = new $JssorSlider$("jssor_1", jssor_1_options);

 //      /*#region responsive code begin*/

 //      var MAX_WIDTH = 3000;

 //      function ScaleSlider() {
 //          var containerElement = jssor_1_slider.$Elmt.parentNode;
 //          var containerWidth = containerElement.clientWidth;

 //          if (containerWidth) {

 //              var expectedWidth = Math.min(MAX_WIDTH || containerWidth, containerWidth);

 //              jssor_1_slider.$ScaleWidth(expectedWidth);
 //          }
 //          else {
 //              window.setTimeout(ScaleSlider, 30);
 //          }
 //      }

 //      ScaleSlider();

 //      $Jssor$.$AddEvent(window, "load", ScaleSlider);
 //      $Jssor$.$AddEvent(window, "resize", ScaleSlider);
 //      $Jssor$.$AddEvent(window, "orientationchange", ScaleSlider);
 //      /*#endregion responsive code end*/
 //  };
 //  jssor_1_slider_init2();

  function  jssor_1_slider_init() {

            var jssor_1_options = {
              $AutoPlay: 1,
              $ArrowNavigatorOptions: {
                $Class: $JssorArrowNavigator$
              },
              $ThumbnailNavigatorOptions: {
                $Class: $JssorThumbnailNavigator$,
                $SpacingX: 5,
                $SpacingY: 5
              }
            };

            var jssor_1_slider = new $JssorSlider$("jssor3", jssor_1_options);

            /*#region responsive code begin*/

            var MAX_WIDTH = 980;

            function ScaleSlider() {
                var containerElement = jssor_1_slider.$Elmt.parentNode;
                var containerWidth = containerElement.clientWidth;

                if (containerWidth) {

                    var expectedWidth = Math.min(MAX_WIDTH || containerWidth, containerWidth);

                    jssor_1_slider.$ScaleWidth(expectedWidth);
                }
                else {
                    window.setTimeout(ScaleSlider, 30);
                }
            }

            ScaleSlider();

            $Jssor$.$AddEvent(window, "load", ScaleSlider);
            $Jssor$.$AddEvent(window, "resize", ScaleSlider);
            $Jssor$.$AddEvent(window, "orientationchange", ScaleSlider);
            /*#endregion responsive code end*/
        };
        // jssor_1_slider_init();
});

