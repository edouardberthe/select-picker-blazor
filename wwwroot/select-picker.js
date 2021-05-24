window.InitSelectPicker = (dotnetHelper, callbackMethodName, pickerElementName) => {
  // initialize the specified picker element
  $(pickerElementName).selectpicker();
  // setup event to push the selected dropdown value back to c# code
  $(pickerElementName).on('changed.bs.select', function (e, clickedIndex, isSelected, previousValue) {
      dotnetHelper.invokeMethodAsync(callbackMethodName, $(pickerElementName).val());
  });
}
