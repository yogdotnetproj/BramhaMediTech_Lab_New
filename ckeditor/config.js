/**
 * @license Copyright (c) 2003-2018, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

  CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
      // config.uiColor = '#AADC6E';
      config.extraPlugins = 'autogrow';
      config.autoGrow_onStartup = true;
    config.disableNativeSpellChecker = false;
    config.height = 1700;
    config.height = '170em';
   // config.width = 650;
   // config.width = '65em';


};
  CKEDITOR.editorConfig = function (config) {

      config.filebrowserBrowseUrl = 'javascript:void(0)';

  };