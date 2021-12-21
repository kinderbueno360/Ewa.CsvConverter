using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Ewa.CsvConverter.Model
{
    public class ItemMap<TClass, TItem>
    {
        public Func<TClass, string>? itemDefinition;
        public readonly MemberInfo member;

        public ItemMap(MemberInfo member)
        {
            this.member = member;
        }

        private ItemMap(MemberInfo member, Func<TClass, string> itemDefinition)
        {
            this.member = member;
            this.itemDefinition = itemDefinition;
        }

        public virtual ItemMap<TClass, TItem> Value(Func<TClass, string> itemDefinition)
        {
            this.itemDefinition = itemDefinition;
            return this;
        }
    }
}
