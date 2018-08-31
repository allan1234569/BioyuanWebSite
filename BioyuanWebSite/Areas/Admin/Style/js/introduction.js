$(function () {

    var addIntroductionEditor = CKEDITOR.replace("TextArea1");
    var addPurposeEditor = CKEDITOR.replace("TextArea2");
    var addVisionEditor = CKEDITOR.replace("TextArea3");

    var modifyIntroductionEditor = CKEDITOR.replace("ModifyTextArea1");
    var modifyPurposeEditor = CKEDITOR.replace("ModifyTextArea2");
    var modifyVisionEditor = CKEDITOR.replace("ModifyTextArea3");

    $('#addIntroductionModal').modal({
        backdrop: 'static',
        keyboard: false,
        show: false
    });
    $('#modifyIntroductionModal').modal({
        backdrop: 'static',
        keyboard: false,
        show: false
    });

    $("addIntroductionModal").on('hidden.bs.modal', function () {
        addIntroductionEditor.setData("");
        addPurposeEditor.setData("");
        addVisionEditor.setData("");
    })

    $("modifyIntroductionModal").on('hidden.bs.modal', function () {
        $("#ModifyId").val("");
        modifyIntroductionEditor.setData("");
        modifyPurposeEditor.setData("");
        modifyVisionEditor.setData("");
    })

    $("#modifyIntroduction_btn").on('click', function () {
        
        $.ajax({
            type: 'post',
            url: '/Admin/Home/IntroductionManage/GetIntroductionDetail',
            async: false,
            dataType: 'text',
            success: function (data) {
                
                var jsonObj = JSON.parse(data);
                 
                //{"companyIntroduction":"这是公司简介","corporatePurpose":"<br \/>\u000d\u000a这是企业宗旨","corporateVision":"<br \/>\u000d\u000a这是企业愿景","id":null}
                
                $("#ModifyId").val(jsonObj.id);
                modifyIntroductionEditor.setData(jsonObj.companyIntroduction);
                modifyPurposeEditor.setData(jsonObj.corporatePurpose);
                modifyVisionEditor.setData(jsonObj.corporateVision);

                $("#modifyIntroductionModal").modal('show');
            }
        });

    })
});







