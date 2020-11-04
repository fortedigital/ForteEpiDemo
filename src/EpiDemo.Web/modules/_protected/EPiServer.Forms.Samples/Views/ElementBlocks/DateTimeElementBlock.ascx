<%@ import namespace="EPiServer.Forms.Helpers.Internal" %>
<%@ import namespace="EPiServer.Forms.Samples.Implementation.Elements" %>
<%@ import namespace="EPiServer.Forms.Samples.EditView" %>

<%@ control language="C#" inherits="ViewUserControl<DateTimeElementBlock>" %>

<%
    var formElement = Model.FormElement;
    var labelText = Model.Label;
    var errorMessage = Model.GetErrorMessage();
    var pickerType = ((DateTimePickerType) Model.PickerType).ToString().ToLower();

    var defaultValue = Model.GetDefaultValue();
    var values = !string.IsNullOrEmpty(defaultValue) ? defaultValue.Split('|') : null;
    var startValue = (values != null && values.Length > 0) ? values[0] : null;
    var endValue = (values != null && values.Length > 1) ? values[1] : null;
    var placeHolders = !string.IsNullOrEmpty(Model.PlaceHolder) ? Model.PlaceHolder.Split('|') : null;
    var startPlaceHolder = (placeHolders != null && placeHolders.Length > 0) ? placeHolders[0] : null;
    var endPlaceHolder = (placeHolders != null && placeHolders.Length > 1) ? placeHolders[1] : null;
    var hours = Enumerable.Range(1, 24)
        .Select(n => n < 10 ? "0" + n.ToString() : n.ToString())
        .ToList();
    var minutes = new string[4] {"00", "15", "30", "45"};
%>

<% if (pickerType == "timepicker")
   { %>
    <div class="Form__Element Form__CustomElement FormDateTime FormTime FormTimeSelection <%: Model.GetValidationCssClasses() %>" data-epiforms-element-name="<%: formElement.ElementName %>">
        <label for="<%: formElement.Guid %>" class="Form__Element__Caption"><%: labelText %></label>
        <input name="<%: formElement.ElementName %>" id="<%: formElement.Guid %>" type="hidden" class="Form__CustomInput FormDateTime__Input"
               placeholder="<%: Model.PlaceHolder %>" value="<%: Model.GetDefaultValue() %>" <%= Model.AttributesString %>/>

        <div class="FormTime__Container">
            <div class="FormTime__InputWrapper SelectInputWrapper">
                <select id="<%: formElement.Guid + "_hours" %>" class="Form__CustomInput FormDateTime__hour" onchange="onInputChange()">
                    <%
                        var defaultOptionSelectedStart = !string.IsNullOrEmpty(startPlaceHolder) && string.IsNullOrEmpty(startValue) ? "selected" : string.Empty;
                        var defaultOptionItemTextStart = !string.IsNullOrEmpty(startPlaceHolder) && string.IsNullOrEmpty(startValue) ? startPlaceHolder : string.Empty;
                    %>
                    <option disabled="disabled" <%= defaultOptionSelectedStart %> value=""><%: defaultOptionItemTextStart %></option>
                    <%
                        foreach (var item in hours)
                        {
                            var selectedString = !string.IsNullOrEmpty(startValue) && startValue == item ? "selected" : string.Empty;
                    %>
                        <option value="<%: item %>" <%= selectedString %>>
                            <%: item %>
                        </option>
                    <% } %>
                </select>
            </div>
            <span class="FormTime__Separator">:</span>
            <div class="FormTime__InputWrapper SelectInputWrapper">
                <select id="<%: formElement.Guid + "_minutes" %>" class="Form__CustomInput FormDateTime__hour" onchange="onInputChange()">
                    <%
                        var defaultOptionSelectedEnd = !string.IsNullOrEmpty(endPlaceHolder) && string.IsNullOrEmpty(endValue) ? "selected" : string.Empty;
                        var defaultOptionItemTextEnd = !string.IsNullOrEmpty(endPlaceHolder) && string.IsNullOrEmpty(endValue) ? endPlaceHolder : string.Empty;
                    %>

                    <option disabled="disabled" <%= defaultOptionSelectedEnd %> value=""><%: defaultOptionItemTextEnd %></option>
                    <%
                        foreach (var item in minutes)
                        {
                            var selectedString = !string.IsNullOrEmpty(startValue) && startValue == item ? "selected" : string.Empty;
                    %>
                        <option value="<%: item %>" <%= selectedString %>>
                            <%: item %>
                        </option>
                    <% } %>
                </select>
            </div>
        </div>

        <script>
             function onInputChange() {
                var hoursInputId = "<%: formElement.Guid + "_hours" %>";
                var hoursInput = document.getElementById(hoursInputId);
                var hour = hoursInput.options[hoursInput.selectedIndex].value || "00";
                
                var minutesInputId = "<%: formElement.Guid + "_minutes" %>";
                var minutesInput = document.getElementById(minutesInputId);
                var minutes = minutesInput.options[minutesInput.selectedIndex].value || "00";
                
                var hiddenInputId = "<%: formElement.Guid %>";
                var hiddenInput = document.getElementById(hiddenInputId);
                hiddenInput.value =  timeConvert (hour + ":" + minutes);
             }
             
             function timeConvert (time) {
               time = time.toString ().match (/^([01]\d|2[0-3])(:)([0-5]\d)(:[0-5]\d)?$/) || [time];
               if (time.length > 1) {
                 time = time.slice (1);
                 time[5] = +time[0] < 12 ? ' AM' : ' PM';
                 time[0] = +time[0] % 12 || 12;
                 time[0] = time[0] < 10 ? "0" + time[0] : time[0]; 
               }
               return time.join ('');
             }
        </script>
        <span data-epiforms-linked-name="<%: formElement.ElementName %>" class="Form__Element__ValidationError" style="<%: string.IsNullOrEmpty(errorMessage) ? "display:none" : "" %>;"><%: errorMessage %></span>
    </div>
<% }
   else
   { %>
    <div class="Form__Element Form__CustomElement FormDateTime <%: Model.GetValidationCssClasses() %>" data-epiforms-element-name="<%: formElement.ElementName %>">
        <label for="<%: formElement.Guid %>" class="Form__Element__Caption"><%: labelText %></label>
        <div class="FormDateTimeRange__InputWrapper DateInputWrapper">
            <input name="<%: formElement.ElementName %>" id="<%: formElement.Guid %>" type="text" class="Form__CustomInput FormDateTime__Input"
                   placeholder="<%: Model.PlaceHolder %>" value="<%: Model.GetDefaultValue() %>" <%= Model.AttributesString %>/>
        </div>
        <span data-epiforms-linked-name="<%: formElement.ElementName %>" class="Form__Element__ValidationError" style="<%: string.IsNullOrEmpty(errorMessage) ? "display:none" : "" %>;"><%: errorMessage %></span>
    </div>
<% } %>

<% if (!EPiServer.Editor.PageEditing.PageIsInEditMode)
   {
%>
    <script type="text/javascript">
        var __SamplesDateTimeElements = __SamplesDateTimeElements || [];
        __SamplesDateTimeElements.push({
            guid: "<%: formElement.Guid %>",
                pickerType: "<%: pickerType %>"
            });
    </script>
<% } %>