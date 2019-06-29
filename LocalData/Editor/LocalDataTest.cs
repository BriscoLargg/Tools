﻿using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Object = System.Object;

public class LocalDataTest : MonoBehaviour
{
    [TearDown]
    public void TearDown()
    {
        LocalData.DeleteAll();
    }
    
    //------------------------------------------------------------------------------------------------------------------
    
    [Test]
    public void StoreDataTest()
    {
        const string id = "StoreDataTest";
        const string id1 = "StoreDataTest1";
        const string id2 = "StoreDataTest2";
        //clean data
        var testData = new TestLocalData();

        //new data
        var newInt = int.MaxValue;
        var newBool = true;
        var newFloat = 123.123141f;
        var newString = "ycaro";
        
        //change test data
        testData.TestInt = newInt;
        testData.TestBool = newBool;
        testData.TestNestedData = new TestLocalData.NestedLocalData()
        {
            TestFloat = newFloat,
            TestString = newString
        };
        
        //store the data locally
        LocalData.StoreData(testData, id);
        //store the data locally
        LocalData.StoreData(testData, id1);
        //store the data locally
        LocalData.StoreData(testData, id2);
            
        //get the stored data
        var retrievedData = LocalData.LoadData<TestLocalData>(id);
        
        //compare data
        Assert.True(testData.IsEqual(retrievedData));
        LocalData.SaveCopyAtAssets();
    }

    [Test]
    public void RemoveDataTest()
    {
        const string id = "RemoveDataTest";
        var testData = new TestLocalData();
        LocalData.StoreData(testData, id);
        LocalData.DeleteData(id);
        Assert.False(LocalData.HasData(id));
    }
    
    [Test]
    public void HasDataTest()
    {
        const string id = "HasDataTest";
        var testData = new TestLocalData();
        
        //yes
        LocalData.StoreData(testData, id);
        Assert.True(LocalData.HasData(id));
        
        //no
        LocalData.DeleteData(id);
        Assert.False(LocalData.HasData(id));
    }
    
    //------------------------------------------------------------------------------------------------------------------

    [Serializable]
    public class TestLocalData
    {
        public int TestInt;
        public bool TestBool;
        public NestedLocalData TestNestedData;

        [Serializable]
        public class NestedLocalData
        {
            public float TestFloat;
            public string TestString;

            public bool IsEqual(NestedLocalData other)
            {
                return Math.Abs(TestFloat - other.TestFloat) < 0.01f && TestString == other.TestString;
            }

            public override string ToString()
            {
                var log = string.Empty;
                log += TestFloat;
                log += TestString;
                return log;
            }
        }

        public bool IsEqual(TestLocalData other)
        {
            return TestBool == other.TestBool
                   && TestInt == other.TestInt
                   && TestNestedData.IsEqual(other.TestNestedData);
        }

        public override string ToString()
        {
            var log = string.Empty;
            log += TestInt;
            log += TestBool;
            log += TestNestedData;
            return log;
        }
    }
}
