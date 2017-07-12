using System;
using Foundation;
using ObjCRuntime;

namespace FBKVOControllerNS
{
	[Static]
	partial interface Constants
	{
		// extern NSString *const _Nonnull FBKVONotificationKeyPathKey;
		[Field("FBKVONotificationKeyPathKey", "__Internal")]
		NSString FBKVONotificationKeyPathKey { get; }
	}

	// typedef void (^FBKVONotificationBlock)(id _Nullable, id _Nonnull, NSDictionary<NSString *,id> * _Nonnull);
	delegate void FBKVONotificationBlock([NullAllowed] NSObject arg0, NSObject arg1, NSDictionary<NSString, NSObject> arg2);

	// @interface FBKVOController : NSObject
	[BaseType(typeof(NSObject))]
	[DisableDefaultCtor]
	interface FBKVOController
	{
		// +(instancetype _Nonnull)controllerWithObserver:(id _Nullable)observer;
		[Static]
		[Export("controllerWithObserver:")]
		FBKVOController ControllerWithObserver([NullAllowed] NSObject observer);

		// -(instancetype _Nonnull)initWithObserver:(id _Nullable)observer retainObserved:(BOOL)retainObserved __attribute__((objc_designated_initializer));
		[Export("initWithObserver:retainObserved:")]
		[DesignatedInitializer]
		IntPtr Constructor([NullAllowed] NSObject observer, bool retainObserved);

		// -(instancetype _Nonnull)initWithObserver:(id _Nullable)observer;
		[Export("initWithObserver:")]
		IntPtr Constructor([NullAllowed] NSObject observer);

		// @property (readonly, nonatomic, weak) id _Nullable observer;
		[NullAllowed, Export("observer", ArgumentSemantic.Weak)]
		NSObject Observer { get; }

		// -(void)observe:(id _Nullable)object keyPath:(NSString * _Nonnull)keyPath options:(NSKeyValueObservingOptions)options block:(FBKVONotificationBlock _Nonnull)block;
		[Export("observe:keyPath:options:block:")]
		void Observe([NullAllowed] NSObject @object, string keyPath, NSKeyValueObservingOptions options, FBKVONotificationBlock block);

		// -(void)observe:(id _Nullable)object keyPath:(NSString * _Nonnull)keyPath options:(NSKeyValueObservingOptions)options action:(SEL _Nonnull)action;
		[Export("observe:keyPath:options:action:")]
		void Observe([NullAllowed] NSObject @object, string keyPath, NSKeyValueObservingOptions options, Selector action);

		// -(void)observe:(id _Nullable)object keyPath:(NSString * _Nonnull)keyPath options:(NSKeyValueObservingOptions)options context:(void * _Nullable)context;
		[Export("observe:keyPath:options:context:")]
		unsafe void Observe([NullAllowed] NSObject @object, string keyPath, NSKeyValueObservingOptions options, [NullAllowed] NSObject context);

		// -(void)observe:(id _Nullable)object keyPaths:(NSArray<NSString *> * _Nonnull)keyPaths options:(NSKeyValueObservingOptions)options block:(FBKVONotificationBlock _Nonnull)block;
		[Export("observe:keyPaths:options:block:")]
		void Observe([NullAllowed] NSObject @object, string[] keyPaths, NSKeyValueObservingOptions options, FBKVONotificationBlock block);

		// -(void)observe:(id _Nullable)object keyPaths:(NSArray<NSString *> * _Nonnull)keyPaths options:(NSKeyValueObservingOptions)options action:(SEL _Nonnull)action;
		[Export("observe:keyPaths:options:action:")]
		void Observe([NullAllowed] NSObject @object, string[] keyPaths, NSKeyValueObservingOptions options, Selector action);

		// -(void)observe:(id _Nullable)object keyPaths:(NSArray<NSString *> * _Nonnull)keyPaths options:(NSKeyValueObservingOptions)options context:(void * _Nullable)context;
		[Export("observe:keyPaths:options:context:")]
		unsafe void Observe([NullAllowed] NSObject @object, string[] keyPaths, NSKeyValueObservingOptions options, [NullAllowed] NSObject context);

		// -(void)unobserve:(id _Nullable)object keyPath:(NSString * _Nonnull)keyPath;
		[Export("unobserve:keyPath:")]
		void Unobserve([NullAllowed] NSObject @object, string keyPath);

		// -(void)unobserve:(id _Nullable)object;
		[Export("unobserve:")]
		void Unobserve([NullAllowed] NSObject @object);

		// -(void)unobserveAll;
		[Export("unobserveAll")]
		void UnobserveAll();
	}

	// @interface FBKVOController (NSObject)
	[Category]
	[BaseType(typeof(NSObject))]
	interface NSObject_FBKVOController
	{
		// @property (nonatomic, strong) FBKVOController * _Nonnull KVOController;
		[Export("KVOController", ArgumentSemantic.Strong)]
		FBKVOController GetKVOController();

		[Export("setKVOController:", ArgumentSemantic.Strong)]
		void SetKVOController(FBKVOController controller);

		// @property (nonatomic, strong) FBKVOController * _Nonnull KVOControllerNonRetaining;
		[Export("KVOControllerNonRetaining", ArgumentSemantic.Strong)]
		FBKVOController GetKVOControllerNonRetaining();

		[Export("setKVOControllerNonRetaining:", ArgumentSemantic.Strong)]
		void KVOControllerNonRetaining(FBKVOController controller);
	}

	//[Static]
	//[Verify(ConstantsInterfaceAssociation)]
	//partial interface Constants
	//{
	//    // extern double KVOControllerVersionNumber;
	//    [Field("KVOControllerVersionNumber", "__Internal")]
	//    double KVOControllerVersionNumber { get; }

	//    // extern const unsigned char [] KVOControllerVersionString;
	//    [Field("KVOControllerVersionString", "__Internal")]
	//    byte[] KVOControllerVersionString { get; }
	//}
}
