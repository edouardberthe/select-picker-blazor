using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace SelectPicker.Components
{
    public abstract class SelectPickerBase<TItem> : ComponentBase
    {
        private static readonly Random randomGenerator = new Random();

        [Parameter]
        public string Id { get; set; } = randomGenerator.Next().ToString();

        [Parameter]
        public List<TItem> Items { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> Attributes { get; set; }

        [Parameter]
        public RenderFragment<TItem> OptionTemplate { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        // call the javascript method to init the select picker
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender) // only needs to be called once per page render
            {
                await JSRuntime.InvokeVoidAsync("InitSelectPicker", DotNetObjectReference.Create(this),
                    nameof(OnSelectedValue), $"#{Id}");
            }
        }

        [JSInvokable]
        public abstract void OnSelectedValue(JsonElement val);
    }
}