/**
 * @license Copyright (c) 2003-2015, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */
var URL_APPLICATION = $('#URL_APPLICATION').val();
CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    // config.uiColor = '#AADC6E';

    config.syntaxhighlight_lang = 'csharp';
    config.syntaxhighlight_hideControls = true;
    config.language = 'vi';
    config.entities_latin = false;
    config.filebrowserBrowseUrl = URL_APPLICATION + '/Scripts/plugins/ckfinder_2.6.3/ckfinder.html';
    config.filebrowserImageBrowseUrl = URL_APPLICATION + '/Scripts/plugins/ckfinder_2.6.3/ckfinder.html?Type=Images';
    config.filebrowserFlashBrowseUrl = URL_APPLICATION + '/Scripts/plugins/ckfinder_2.6.3/ckfinder.html?Type=Flash';
    config.filebrowserUploadUrl = URL_APPLICATION + '/Scripts/plugins/ckfinder_2.6.3/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserImageUploadUrl = URL_APPLICATION + '/Data';
    config.filebrowserFlashUploadUrl = URL_APPLICATION + '/Scripts/plugins/ckfinder_2.6.3/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';

    CKFinder.setupCKEditor(null, URL_APPLICATION + '/Scripts/plugins/ckfinder_2.6.3/');
};