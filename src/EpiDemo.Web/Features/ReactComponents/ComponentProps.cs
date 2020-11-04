using Reinforced.Typings.Attributes;

namespace EpiDemo.Web.Features.ReactComponents
{
    [TsInterface]
    public class ComponentProps<T>
    {
        public T Model { get; set; }

        public ComponentProps(T model)
        {
            this.Model = model;
        }
    }
}