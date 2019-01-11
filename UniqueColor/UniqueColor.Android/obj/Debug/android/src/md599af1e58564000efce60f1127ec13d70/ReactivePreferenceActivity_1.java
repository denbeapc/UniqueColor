package md599af1e58564000efce60f1127ec13d70;


public class ReactivePreferenceActivity_1
	extends md599af1e58564000efce60f1127ec13d70.ReactivePreferenceActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("ReactiveUI.ReactivePreferenceActivity`1, ReactiveUI", ReactivePreferenceActivity_1.class, __md_methods);
	}


	public ReactivePreferenceActivity_1 ()
	{
		super ();
		if (getClass () == ReactivePreferenceActivity_1.class)
			mono.android.TypeManager.Activate ("ReactiveUI.ReactivePreferenceActivity`1, ReactiveUI", "", this, new java.lang.Object[] {  });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
