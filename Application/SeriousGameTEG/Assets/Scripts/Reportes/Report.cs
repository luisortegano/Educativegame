using System;
using System.Collections;

/**
Base Class for report objects 

virtual methos are dospised for basic behavior
**/

public interface Report {
	void setUserId (int UserId);
	IEnumerator copyHtmlToIndex();
}