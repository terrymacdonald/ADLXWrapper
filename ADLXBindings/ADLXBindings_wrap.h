/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (https://www.swig.org).
 * Version 4.3.0
 *
 * Do not make changes to this file unless you know what you are doing - modify
 * the SWIG interface file instead.
 * ----------------------------------------------------------------------------- */

#ifndef SWIG_ADLX_WRAP_H_
#define SWIG_ADLX_WRAP_H_

class SwigDirector_IADLXDisplayListChangedListener : public adlx::IADLXDisplayListChangedListener, public Swig::Director {

public:
    SwigDirector_IADLXDisplayListChangedListener();
    virtual adlx_bool OnDisplayListChanged(adlx::IADLXDisplayList *pNewDisplay);

    typedef unsigned int (SWIGSTDCALL* SWIG_Callback0_t)(void *);
    void swig_connect_director(SWIG_Callback0_t callbackOnDisplayListChanged);

private:
    SWIG_Callback0_t swig_callbackOnDisplayListChanged;
    void swig_init_callbacks();
};


#endif
