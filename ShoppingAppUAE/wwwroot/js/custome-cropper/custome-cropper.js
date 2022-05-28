 function customimageCropper (data){
            const FILE_ID = $(data.fileSelector);
            const SOURCE_IMAGE_MODEL = $(data.modelImageSelector);
            const SOURCE_IMAGE =$(data.sourceImageSelector);
            const SUBMIT_SELECTOR = $(data.submitSelector);
            const CROPPER_MODAL = $('#modal-default');

            $(FILE_ID).change(function () {
            
            var file = this.files[0];
            var reader = new FileReader();
            reader.onload = function (e) {
                // destroy the cropper if exists
                if ($(SOURCE_IMAGE_MODEL).data('cropper')) {
                    $(SOURCE_IMAGE_MODEL).cropper('destroy');
                }

                $(SOURCE_IMAGE_MODEL).attr('src', e.target.result);
                $(CROPPER_MODAL).modal("show");
                 $image = $(SOURCE_IMAGE_MODEL);
                $image.cropper({
                    aspectRatio: data.aspectRatio == null? (1 / 1) : data.aspectRatio,
                 modal:true,
                 width:400,
                 width:400,
                 minContainerWidth: data.minContainerWidth == null ? 500: data.minContainerWidth ,
                    minContainerHeight:data.minContainerHeight == null ? 500: data.minContainerHeight,
                    maxCropBoxWidth: data.maxCropBoxWidth == null ? 400: data.maxCropBoxWidth,
                 maxCropBoxHeight: data.maxCropBoxWidth == null ? 400: data.maxCropBoxWidth,
                    cropBoxResizable: false,
                 viewMode: 1,   
                    dragMode: 'move',
             crop: function(event) {
                  $imgData = event.detail;
            //    console.log(event.detail.x);
            // console.log(event.detail.y);
            // console.log(event.detail.width);
            // console.log(event.detail.height);
            // console.log(event.detail.rotate);
            // console.log(event.detail.scaleX);
            // console.log(event.detail.scaleY);
        }
        });
            }
            reader.readAsDataURL(file);
        });

        $(SUBMIT_SELECTOR).on("click",function(){
            var canvas = $image.cropper('getCroppedCanvas');
              var base64url = canvas.toDataURL('image/jpeg');
     
         var base64 = base64url.split(',')[1];
           
            
              // resize the image 
                var image = new Image();
                image.src = base64url;
             
                image.onload = function() {
                    var MAX_WIDTH = data.maxCropBoxWidth;
                    var MAX_HEIGHT = data.maxCropBoxHeight;
                    var width = image.width;
                    var height = image.height;
                    if (width > height) {
                        if (width > MAX_WIDTH) {
                            height *= MAX_WIDTH / width;
                            width = MAX_WIDTH;
                        }
                    } else {
                        if (height > MAX_HEIGHT) {
                            width *= MAX_HEIGHT / height;
                            height = MAX_HEIGHT;
                        }
                    }
                    var canvas = document.createElement('canvas');
                    canvas.width = width;
                    canvas.height = height;
                    var ctx = canvas.getContext("2d");
                    ctx.drawImage(image, 0, 0, width, height);
                    var dataurl = canvas.toDataURL("image/jpeg");
                    // fix the the url to be used in img tag
                    var url = dataurl.replace(/^data:image\/(png|jpg);base64,/, "");
                    $(SOURCE_IMAGE).attr('src', url);
                    $(CROPPER_MODAL).modal("hide");
                }
            $(SOURCE_IMAGE).attr("src",base64url);
            // empty the file input
            $(FILE_ID).val('');

            // close the modal
            $(CROPPER_MODAL).modal("hide");
           });
    }

