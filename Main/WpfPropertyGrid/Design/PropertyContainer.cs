namespace System.Windows.Controls.WpfPropertyGrid
{
    /// <summary>
    /// Specialized UI container for a property entry.
    /// </summary>
    public class PropertyContainer : GridEntryContainer
    {
        /// <summary>
        /// Initializes the <see cref="PropertyContainer"/> class.
        /// </summary>
        static PropertyContainer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyContainer), new FrameworkPropertyMetadata(typeof(PropertyContainer)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyContainer"/> class.
        /// </summary>
        public PropertyContainer()
        {
            SetParentContainer(this, this);
            
            //PreviewMouseDown+=(sender, args) =>  MessageBox.Show(Entry.Name);
        }
    }
}
