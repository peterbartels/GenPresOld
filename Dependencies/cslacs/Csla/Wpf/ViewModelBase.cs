﻿using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Reflection;
using Csla.Reflection;
using Csla.Security;
using Csla.Core;
using System.Collections;

#if SILVERLIGHT
namespace Csla.Silverlight
#else
namespace Csla.Wpf
#endif
{
  /// <summary>
  /// Base class used to create ViewModel objects that
  /// implement their own commands/verbs/actions.
  /// </summary>
  /// <typeparam name="T">Type of the Model object.</typeparam>
#if SILVERLIGHT
  public abstract class ViewModelBase<T> : FrameworkElement,
    INotifyPropertyChanged
#else
  public abstract class ViewModelBase<T> : DependencyObject,
    INotifyPropertyChanged
#endif
  {
    #region Constructor

    /// <summary>
    /// Create new instance of base class used to create ViewModel objects that
    /// implement their own commands/verbs/actions.
    /// </summary>
    public ViewModelBase()
    {
      SetPropertiesAtObjectLevel();
    }
    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the Model object.
    /// </summary>
    public static readonly DependencyProperty ModelProperty =
        DependencyProperty.Register("Model", typeof(T), typeof(ViewModelBase<T>),
        new PropertyMetadata((o, e) => 
        {
          var viewmodel = (ViewModelBase<T>)o;
          viewmodel.OnModelChanged((T)e.OldValue, (T)e.NewValue);
          if (viewmodel.ManageObjectLifetime)
          {
            var undo = e.NewValue as Csla.Core.ISupportUndo;
            if (undo != null)
              undo.BeginEdit();
          }
        }));
    /// <summary>
    /// Gets or sets the Model object.
    /// </summary>
    public T Model
    {
      get { return (T)GetValue(ModelProperty); }
      set { SetValue(ModelProperty, value); }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the
    /// ViewModel should automatically managed the
    /// lifetime of the Model.
    /// </summary>
    public static readonly DependencyProperty ManageObjectLifetimeProperty =
        DependencyProperty.Register("ManageObjectLifetime", typeof(bool),
        typeof(ViewModelBase<T>), new PropertyMetadata(true));
    /// <summary>
    /// Gets or sets a value indicating whether the
    /// ViewManageObjectLifetime should automatically managed the
    /// lifetime of the ManageObjectLifetime.
    /// </summary>
    public bool ManageObjectLifetime
    {
      get { return (bool)GetValue(ManageObjectLifetimeProperty); }
      set { SetValue(ManageObjectLifetimeProperty, value); }
    }

    private Exception _error;

    /// <summary>
    /// Gets the Error object corresponding to the
    /// last asyncronous operation.
    /// </summary>
    public Exception Error
    {
      get { return _error; }
      protected set
      {
        if (!ReferenceEquals(_error, value))
        {
          _error = value;
          OnPropertyChanged("Error");
          if (_error != null)
            OnError(_error);
        }
      }
    }

    /// <summary>
    /// Event raised when an error occurs during processing.
    /// </summary>
    public event EventHandler<ErrorEventArgs> ErrorOccurred;

    /// <summary>
    /// Raises ErrorOccurred event when an error occurs
    /// during processing.
    /// </summary>
    /// <param name="error"></param>
    protected virtual void OnError(Exception error)
    {
      if (ErrorOccurred != null)
        ErrorOccurred(this, new ErrorEventArgs { Error = error });
    }

    private bool _isBusy;

    /// <summary>
    /// Gets a value indicating whether this object is
    /// executing an asynchronous process.
    /// </summary>
    public bool IsBusy
    {
      get { return _isBusy; }
      protected set
      {
        _isBusy = value;
        OnPropertyChanged("IsBusy");
        SetProperties();
      }
    }

    #endregion

    #region Can___ properties

    private bool _canSave = false;

    /// <summary>
    /// Gets a value indicating whether the Model
    /// can currently be saved.
    /// </summary>
    public bool CanSave
    {
      get
      {
        return _canSave;
      }
      private set
      {
        if (_canSave != value)
        {
          _canSave = value;
          OnPropertyChanged("CanSave");
        }
      }
    }

    private bool _canCancel = false;

    /// <summary>
    /// Gets a value indicating whether the Model
    /// can currently be canceled.
    /// </summary>
    public bool CanCancel
    {
      get
      {
        return _canCancel;
      }
      private set
      {
        if (_canCancel != value)
        {
          _canCancel = value;
          OnPropertyChanged("CanCancel");
        }
      }
    }

    private bool _canCreate = false;

    /// <summary>
    /// Gets a value indicating whether an instance
    /// of the Model
    /// can currently be created.
    /// </summary>
    public bool CanCreate
    {
      get
      {
        return _canCreate;
      }
      private set
      {
        if (_canCreate != value)
        {
          _canCreate = value;
          OnPropertyChanged("CanCreate");
        }
      }
    }

    private bool _canDelete = false;

    /// <summary>
    /// Gets a value indicating whether the Model
    /// can currently be deleted.
    /// </summary>
    public bool CanDelete
    {
      get
      {
        return _canDelete;
      }
      private set
      {
        if (_canDelete != value)
        {
          _canDelete = value;
          OnPropertyChanged("CanDelete");
        }
      }
    }

    private bool _canFetch = false;

    /// <summary>
    /// Gets a value indicating whether an instance
    /// of the Model
    /// can currently be retrieved.
    /// </summary>
    public bool CanFetch
    {
      get
      {
        return _canFetch;
      }
      private set
      {
        if (_canFetch != value)
        {
          _canFetch = value;
          OnPropertyChanged("CanFetch");
        }
      }
    }

    private bool _canRemove = false;

    /// <summary>
    /// Gets a value indicating whether the Model
    /// can currently be removed.
    /// </summary>
    public bool CanRemove
    {
      get
      {
        return _canRemove;
      }
      private set
      {
        if (_canRemove != value)
        {
          _canRemove = value;
          OnPropertyChanged("CanRemove");
        }
      }
    }

    private bool _canAddNew = false;

    /// <summary>
    /// Gets a value indicating whether the Model
    /// can currently be added.
    /// </summary>
    public bool CanAddNew
    {
      get
      {
        return _canAddNew;
      }
      private set
      {
        if (_canAddNew != value)
        {
          _canAddNew = value;
          OnPropertyChanged("CanAddNew");
        }
      }
    }

    private void SetProperties()
    {
      ITrackStatus targetObject = Model as ITrackStatus;
      ICollection list = Model as ICollection;
      INotifyBusy busyObject = Model as INotifyBusy;
      bool isObjectBusy = false;
      if (busyObject != null && busyObject.IsBusy)
        isObjectBusy = true;
      if (Model != null && targetObject != null)
      {

        if (CanEditObject && targetObject.IsSavable)
          CanSave = true;
        else
          CanSave = false;

        if (CanEditObject && targetObject.IsDirty && !isObjectBusy)
          CanCancel = true;
        else
          CanCancel = false;

        if (CanCreateObject && !targetObject.IsDirty && !isObjectBusy)
          CanCreate = true;
        else
          CanCreate = false;

        if (CanDeleteObject && !isObjectBusy)
          CanDelete = true;
        else
          CanDelete = false;

        if (CanGetObject && !targetObject.IsDirty && !isObjectBusy)
          CanFetch = true;
        else
          CanFetch = false;

        if (list != null)
        {
          Type itemType = Csla.Utilities.GetChildItemType(Model.GetType());
          if (itemType != null)
          {

            if (Csla.Security.AuthorizationRules.CanDeleteObject(itemType) && list.Count > 0 && !isObjectBusy)
              CanRemove = true;
            else
              CanRemove = false;

            if (Csla.Security.AuthorizationRules.CanCreateObject(itemType) && !isObjectBusy)
              CanAddNew = true;
            else
              CanAddNew = false;
          }
          else
          {
            CanAddNew = false;
            CanRemove = false;
          }
        }
        else
        {
          CanRemove = false;
          CanAddNew = false;
        }
      }
      else if (list != null)
      {
        Type itemType = Csla.Utilities.GetChildItemType(Model.GetType());
        if (itemType != null)
        {

          if (Csla.Security.AuthorizationRules.CanDeleteObject(itemType) && list.Count > 0 && !isObjectBusy)
            CanRemove = true;
          else
            CanRemove = false;

          if (Csla.Security.AuthorizationRules.CanCreateObject(itemType) && !isObjectBusy)
            CanAddNew = true;
          else
            CanAddNew = false;
        }
        else
        {
          CanAddNew = false;
          CanRemove = false;
        }
      }
      else
      {
        CanCancel = false;
        CanCreate = false;
        CanDelete = false;
        CanFetch = !IsBusy;
        CanSave = false;
        CanRemove = false;
        CanAddNew = false;
      }
    }

    #endregion

    #region Can methods that only account for user rights

    private bool _canCreateObject;

    /// <summary>
    /// Gets a value indicating whether the current
    /// user is authorized to create a Model.
    /// </summary>
    public bool CanCreateObject
    {
      get { return _canCreateObject; }
      protected set
      {
        if (_canCreateObject != value)
        {
          _canCreateObject = value;
          OnPropertyChanged("CanCreateObject");
        }
      }
    }

    private bool _canGetObject;

    /// <summary>
    /// Gets a value indicating whether the current
    /// user is authorized to retrieve a Model.
    /// </summary>
    public bool CanGetObject
    {
      get { return _canGetObject; }
      protected set
      {
        if (_canGetObject != value)
        {
          _canGetObject = value;
          OnPropertyChanged("CanGetObject");
        }
      }
    }

    private bool _canEditObject;

    /// <summary>
    /// Gets a value indicating whether the current
    /// user is authorized to save (insert or update
    /// a Model.
    /// </summary>
    public bool CanEditObject
    {
      get { return _canEditObject; }
      protected set
      {
        if (_canEditObject != value)
        {
          _canEditObject = value;
          OnPropertyChanged("CanEditObject");
        }
      }
    }

    private bool _canDeleteObject;

    /// <summary>
    /// Gets a value indicating whether the current
    /// user is authorized to delete
    /// a Model.
    /// </summary>
    public bool CanDeleteObject
    {
      get { return _canDeleteObject; }
      protected set
      {
        if (_canDeleteObject != value)
        {
          _canDeleteObject = value;
          OnPropertyChanged("CanDeleteObject");
        }
      }
    }

    private void SetPropertiesAtObjectLevel()
    {
      Type sourceType = typeof(T);

      CanCreateObject = Csla.Security.AuthorizationRules.CanCreateObject(sourceType);
      CanGetObject = Csla.Security.AuthorizationRules.CanGetObject(sourceType);
      CanEditObject = Csla.Security.AuthorizationRules.CanEditObject(sourceType);
      CanDeleteObject = Csla.Security.AuthorizationRules.CanDeleteObject(sourceType);
    }

    #endregion

    #region Verbs

#if !SILVERLIGHT
    /// <summary>
    /// Creates or retrieves a new instance of the 
    /// Model by invoking a static factory method.
    /// </summary>
    /// <param name="factoryMethod">Name of the static factory method.</param>
    /// <param name="factoryParameters">Factory method parameters.</param>
    protected virtual void DoRefresh(string factoryMethod, params object[] factoryParameters)
    {
      if (typeof(T) != null)
      {
        Error = null;
        try
        {
          Model = (T)MethodCaller.CallFactoryMethod(typeof(T), factoryMethod, factoryParameters);
        }
        catch (Exception ex)
        {
          Error = ex;
        }
        OnRefreshed();
      }
    }

    /// <summary>
    /// Creates or retrieves a new instance of the 
    /// Model by invoking a static factory method.
    /// </summary>
    /// <param name="factoryMethod">Name of the static factory method.</param>
    protected virtual void DoRefresh(string factoryMethod)
    {
      DoRefresh(factoryMethod, new object[] { });
    }
#endif

    /// <summary>
    /// Creates or retrieves a new instance of the 
    /// Model by invoking a static factory method.
    /// </summary>
    /// <param name="factoryMethod">Name of the static factory method.</param>
    /// <param name="factoryParameters">Factory method parameters.</param>
    protected virtual void BeginRefresh(string factoryMethod, params object[] factoryParameters)
    {
      if (typeof(T) != null)
        try
        {
          Error = null;
          IsBusy = true;
          var parameters = new List<object>(factoryParameters);
          parameters.Add(CreateHandler(typeof(T)));

          MethodCaller.CallFactoryMethod(typeof(T), factoryMethod, parameters.ToArray());
        }
        catch (Exception ex)
        {
          Error = ex;
          IsBusy = false;
        }
    }

    /// <summary>
    /// Creates or retrieves a new instance of the 
    /// Model by invoking a static factory method.
    /// </summary>
    /// <param name="factoryMethod">Name of the static factory method.</param>
    protected virtual void BeginRefresh(string factoryMethod)
    {
      BeginRefresh(factoryMethod, new object[] { });
    }

    private Delegate CreateHandler(Type objectType)
    {
      var args = typeof(DataPortalResult<>).MakeGenericType(objectType);
      MethodInfo method = MethodCaller.GetNonPublicMethod(GetType(), "QueryCompleted");
      Delegate handler = Delegate.CreateDelegate(typeof(EventHandler<>).MakeGenericType(args), this, method);
      return handler;
    }


    private void QueryCompleted(object sender, EventArgs e)
    {
      IsBusy = false;
      var eventArgs = (IDataPortalResult)e;
      if (eventArgs.Error == null)
        Model = (T)eventArgs.Object;
      else
        Error = eventArgs.Error;
      OnRefreshed();
    }

    /// <summary>
    /// Method called after a refresh operation 
    /// has completed (whether successful or
    /// not).
    /// </summary>
    protected virtual void OnRefreshed()
    { }

#if !SILVERLIGHT
    /// <summary>
    /// Saves the Model, first committing changes
    /// if ManagedObjectLifetime is true.
    /// </summary>
    protected virtual T DoSave()
    {
      T result = (T)Model;
      Error = null;
      try
      {
        var savable = Model as Csla.Core.ISavable;
        if (ManageObjectLifetime)
        {
          // clone the object if possible
          ICloneable clonable = Model as ICloneable;
          if (clonable != null)
            savable = (Csla.Core.ISavable)clonable.Clone();

          //apply changes
          var undoable = savable as Csla.Core.ISupportUndo;
          if (undoable != null)
            undoable.ApplyEdit();
        }

        result = (T)savable.Save();

        Model = result;
        OnSaved();
      }
      catch (Exception ex)
      {
        Error = ex;
        OnSaved();
      }
      return result;
    }
#endif

    /// <summary>
    /// Saves the Model, first committing changes
    /// if ManagedObjectLifetime is true.
    /// </summary>
    protected virtual void BeginSave()
    {
      try
      {
        var savable = Model as Csla.Core.ISavable;
        if (ManageObjectLifetime)
        {
          // clone the object if possible
          ICloneable clonable = Model as ICloneable;
          if (clonable != null)
            savable = (Csla.Core.ISavable)clonable.Clone();

          //apply changes
          var undoable = savable as Csla.Core.ISupportUndo;
          if (undoable != null)
            undoable.ApplyEdit();
        }

        savable.Saved += (o, e) =>
        {
          IsBusy = false;
          if (e.Error == null)
          {
            var result = e.NewObject;
            Model = (T)result;
          }
          else
          {
            Error = e.Error;
          }
          OnSaved();
        };
        Error = null;
        IsBusy = true;
        savable.BeginSave();
      }
      catch (Exception ex)
      {
        IsBusy = false;
        Error = ex;
        OnSaved();
      }
    }

    /// <summary>
    /// Method called after a save operation 
    /// has completed (whether successful or
    /// not).
    /// </summary>
    protected virtual void OnSaved()
    { }

    /// <summary>
    /// Cancels changes made to the model 
    /// if ManagedObjectLifetime is true.
    /// </summary>
    protected virtual void DoCancel()
    {
      if (ManageObjectLifetime)
      {
        var undo = Model as Csla.Core.ISupportUndo;
        if (undo != null)
        {
          undo.CancelEdit();
          undo.BeginEdit();
        }
      }
    }

#if SILVERLIGHT
    /// <summary>
    /// Adds a new item to the Model (if it
    /// is a collection).
    /// </summary>
    protected virtual void BeginAddNew()
    {
      ((IBindingList)Model).AddNew();
      SetProperties();
    }
#else
    /// <summary>
    /// Adds a new item to the Model (if it
    /// is a collection).
    /// </summary>
    protected virtual object DoAddNew()
    {
      var ibl = ((IBindingList)Model);
      var result = ibl.AddNew();
      SetProperties();
      return result;
    }
#endif

    /// <summary>
    /// Removes an item from the Model (if it
    /// is a collection).
    /// </summary>
    protected virtual void DoRemove(object item)
    {
      ((System.Collections.IList)Model).Remove(item);
      SetProperties();
    }

    /// <summary>
    /// Marks the Model for deletion (if it is an
    /// editable root object).
    /// </summary>
    protected virtual void DoDelete()
    {
      ((Csla.Core.IEditableBusinessObject)Model).Delete();
    }

    #endregion

    #region INotifyPropertyChanged Members

    /// <summary>
    /// Event raised when a property changes.
    /// </summary>
    public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Raise the PropertyChanged event.
    /// </summary>
    /// <param name="propertyName">Name of the changed property.</param>
    protected virtual void OnPropertyChanged(string propertyName)
    {
      if (PropertyChanged != null)
        PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
    }

    #endregion

    #region Model Changes Handling

    /// <summary>
    /// Invoked when the Model changes, allowing
    /// event handlers to be unhooked from the old
    /// object and hooked on the new object.
    /// </summary>
    /// <param name="oldValue">Previous Model reference.</param>
    /// <param name="newValue">New Model reference.</param>
    protected virtual void OnModelChanged(T oldValue, T newValue)
    {
      if (ReferenceEquals(oldValue, newValue)) return;

      // unhook events from old value
      if (oldValue != null)
      {
        var npc = oldValue as INotifyPropertyChanged;
        if (npc != null)
          npc.PropertyChanged -= Model_PropertyChanged;
        var ncc = oldValue as INotifyChildChanged;
        if (ncc != null)
          ncc.ChildChanged -= Model_ChildChanged;
        var nb = oldValue as INotifyBusy;
        if (nb != null)
          nb.BusyChanged -= Model_BusyChanged;
      }

      // hook events on new value
      if (newValue != null)
      {
        var npc = newValue as INotifyPropertyChanged;
        if (npc != null)
          npc.PropertyChanged += Model_PropertyChanged;
        var ncc = newValue as INotifyChildChanged;
        if (ncc != null)
          ncc.ChildChanged += Model_ChildChanged;
        var nb = newValue as INotifyBusy;
        if (nb != null)
          nb.BusyChanged += Model_BusyChanged;
      }

      SetProperties();
    }


    private void Model_BusyChanged(object sender, BusyChangedEventArgs e)
    {
      // only set busy state for entire object.  Ignore busy state based
      // on asynch rules being active
      if (e.PropertyName == string.Empty)
        IsBusy = e.Busy;
      else
        SetProperties();
    }


    private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      SetProperties();
    }

    private void Model_ChildChanged(object sender, ChildChangedEventArgs e)
    {
      SetProperties();
    }

    #endregion
  }
}
