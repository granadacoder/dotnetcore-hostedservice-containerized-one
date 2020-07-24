﻿using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyCompany.MyExamples.WorkerServiceExampleOne.Domain.Constants;
using MyCompany.MyExamples.WorkerServiceExampleOne.Domain.Entities;
using MyCompany.MyExamples.WorkerServiceExampleOne.DomainDataLayer.EntityFramework.Contexts;

namespace MyCompany.MyExamples.WorkerServiceExampleOne.DomainDataLayer.EntityFramework.DataGenerators
{
    public static class MyParentDataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new WorkerServiceExampleOneDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<WorkerServiceExampleOneDbContext>>(), serviceProvider.GetRequiredService<Microsoft.Extensions.Logging.ILoggerFactory>()))
            {
                if (context.MyParents.Any())
                {
                    return;   // Database has been seeded
                }

                DateTime parentUpdateDateSeed = DateTime.Parse("01/01/2021");
                int parentCreateCounter = 0;

                Guid aaaaGuid = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
                const string ChildOfAaaaKeyPrefix = "11111111-1111-1111-1111-00000000000";
                const string ChildOfAaaaNamePrefix = "Ch1111-";
                int childACounter = 0;

                Guid bbbbGuid = new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
                const string ChildOfBbbbKeyPrefix = "22222222-2222-2222-2222-00000000000";
                const string ChildOfBbbbNamePrefix = "Ch2222-";
                int childBCounter = 0;

                Guid ccccGuid = new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc");
                const string ChildOfCcccKeyPrefix = "33333333-3333-3333-3333-00000000000";
                const string ChildOfCcccNamePrefix = "Ch3333-";
                int childCCounter = 0;

                Guid ddddGuid = new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd");
                ////string ChildOfDdddKeyPrefix = "44444444-4444-4444-4444-00000000000";
                ////string ChildOfDdddNamePrefix = "Ch4444-";
                ////int childDDounter = 0;

                Guid eeeeGuid = new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee");
                const string ChildOfEeeeKeyPrefix = "55555555-5555-5555-5555-00000000000";
                const string ChildOfEeeeNamePrefix = "Ch5555-";
                int childECounter = 0;

                context.MyParents.AddRange(
                    new MyParentEntity
                    {
                        MyParentKey = aaaaGuid,
                        MyParentName = "ParentA",
                        UpdateDateStamp = parentUpdateDateSeed.AddDays(++parentCreateCounter),
                        MyChilds = new List<MyChildEntity>
                        {
                            new MyChildEntity()
                            {
                                MyChildKey = new Guid(ChildOfAaaaKeyPrefix + Convert.ToString(++childACounter)),
                                MyParentUuidFk = aaaaGuid,
                                MyChildName = ChildOfAaaaNamePrefix + Convert.ToString(childACounter),
                                MyChildMagicStatus = MagicStatusValues.Four,
                                FavoriteColor = Domain.Enums.FavoriteColorEnum.Red,
                                UpdateDateStamp = DateTime.Parse("01/04/2020")
                            },
                            new MyChildEntity()
                            {
                                MyChildKey = new Guid("11111111-1111-1111-1111-00000000000" + Convert.ToString(++childACounter)),
                                MyParentUuidFk = aaaaGuid,
                                MyChildName = ChildOfAaaaNamePrefix + Convert.ToString(childACounter),
                                MyChildMagicStatus = MagicStatusValues.Five,
                                FavoriteColor = Domain.Enums.FavoriteColorEnum.Yellow,
                                UpdateDateStamp = DateTime.Parse("01/05/2020")
                            },
                            new MyChildEntity()
                            {
                                MyChildKey = new Guid("11111111-1111-1111-1111-00000000000" + Convert.ToString(++childACounter)),
                                MyParentUuidFk = aaaaGuid,
                                MyChildName = ChildOfAaaaNamePrefix + Convert.ToString(childACounter),
                                MyChildMagicStatus = MagicStatusValues.Six,
                                FavoriteColor = Domain.Enums.FavoriteColorEnum.Black,
                                UpdateDateStamp = DateTime.Parse("01/06/2020")
                            },
                            new MyChildEntity()
                            {
                                MyChildKey = new Guid("11111111-1111-1111-1111-00000000000" + Convert.ToString(++childACounter)),
                                MyParentUuidFk = aaaaGuid,
                                MyChildName = ChildOfAaaaNamePrefix + Convert.ToString(childACounter),
                                MyChildMagicStatus = MagicStatusValues.NinetyNine,
                                FavoriteColor = Domain.Enums.FavoriteColorEnum.White,
                                UpdateDateStamp = DateTime.Parse("01/31/2020")
                            },
                            new MyChildEntity()
                            {
                                MyChildKey = new Guid("11111111-1111-1111-1111-00000000000" + Convert.ToString(++childACounter)),
                                MyParentUuidFk = aaaaGuid,
                                MyChildName = ChildOfAaaaNamePrefix + Convert.ToString(childACounter),
                                MyChildMagicStatus = MagicStatusValues.Five,
                                FavoriteColor = Domain.Enums.FavoriteColorEnum.Red,
                                UpdateDateStamp = DateTime.Parse("02/05/2020")
                            },
                            new MyChildEntity()
                            {
                                MyChildKey = new Guid("11111111-1111-1111-1111-00000000000" + Convert.ToString(++childACounter)),
                                MyParentUuidFk = aaaaGuid,
                                MyChildName = ChildOfAaaaNamePrefix + Convert.ToString(childACounter),
                                MyChildMagicStatus = MagicStatusValues.Six,
                                FavoriteColor = Domain.Enums.FavoriteColorEnum.Yellow,
                                UpdateDateStamp = DateTime.Parse("02/06/2020")
                            },
                            new MyChildEntity()
                            {
                                MyChildKey = new Guid("11111111-1111-1111-1111-00000000000" + Convert.ToString(++childACounter)),
                                MyParentUuidFk = aaaaGuid,
                                MyChildName = ChildOfAaaaNamePrefix + Convert.ToString(childACounter),
                                MyChildMagicStatus = MagicStatusValues.NinetyNine,
                                FavoriteColor = Domain.Enums.FavoriteColorEnum.Black,
                                UpdateDateStamp = DateTime.Parse("02/29/2020")
                            },
                            new MyChildEntity()
                            {
                                MyChildKey = new Guid("11111111-1111-1111-1111-00000000000" + Convert.ToString(++childACounter)),
                                MyParentUuidFk = aaaaGuid,
                                MyChildName = ChildOfAaaaNamePrefix + Convert.ToString(childACounter),
                                MyChildMagicStatus = MagicStatusValues.NinetyNine,
                                FavoriteColor = Domain.Enums.FavoriteColorEnum.White,
                                UpdateDateStamp = DateTime.Parse("02/29/2020")
                            }
                        }
                    },
                    new MyParentEntity
                    {
                        MyParentKey = bbbbGuid,
                        MyParentName = "ParentB",
                        UpdateDateStamp = parentUpdateDateSeed.AddDays(++parentCreateCounter),
                        MyChilds = new List<MyChildEntity>
                        {
                            new MyChildEntity()
                            {
                                MyChildKey = new Guid(ChildOfBbbbKeyPrefix + Convert.ToString(++childBCounter)),
                                MyParentUuidFk = bbbbGuid,
                                MyChildName = ChildOfBbbbNamePrefix + Convert.ToString(childBCounter),
                                MyChildMagicStatus = MagicStatusValues.Four,
                                FavoriteColor = Domain.Enums.FavoriteColorEnum.Red,
                                UpdateDateStamp = DateTime.Parse("01/04/2020")
                            },
                            new MyChildEntity()
                            {
                                MyChildKey = new Guid(ChildOfBbbbKeyPrefix + Convert.ToString(++childBCounter)),
                                MyParentUuidFk = bbbbGuid,
                                MyChildName = ChildOfBbbbNamePrefix + Convert.ToString(childBCounter),
                                MyChildMagicStatus = MagicStatusValues.Five,
                                FavoriteColor = Domain.Enums.FavoriteColorEnum.Yellow,
                                UpdateDateStamp = DateTime.Parse("01/05/2020")
                            },
                            new MyChildEntity()
                            {
                                MyChildKey = new Guid(ChildOfBbbbKeyPrefix + Convert.ToString(++childBCounter)),
                                MyParentUuidFk = bbbbGuid,
                                MyChildName = ChildOfBbbbNamePrefix + Convert.ToString(childBCounter),
                                MyChildMagicStatus = MagicStatusValues.Six,
                                FavoriteColor = Domain.Enums.FavoriteColorEnum.Black,
                                UpdateDateStamp = DateTime.Parse("01/06/2020")
                            },
                            new MyChildEntity()
                            {
                                MyChildKey = new Guid(ChildOfBbbbKeyPrefix + Convert.ToString(++childBCounter)),
                                MyParentUuidFk = bbbbGuid,
                                MyChildName = ChildOfBbbbNamePrefix + Convert.ToString(childBCounter),
                                MyChildMagicStatus = MagicStatusValues.NinetyNine,
                                FavoriteColor = Domain.Enums.FavoriteColorEnum.White,
                                UpdateDateStamp = DateTime.Parse("01/31/2020")
                            },
                            new MyChildEntity()
                            {
                                MyChildKey = new Guid(ChildOfBbbbKeyPrefix + Convert.ToString(++childBCounter)),
                                MyParentUuidFk = bbbbGuid,
                                MyChildName = ChildOfBbbbNamePrefix + Convert.ToString(childBCounter),
                                MyChildMagicStatus = MagicStatusValues.Five,
                                FavoriteColor = Domain.Enums.FavoriteColorEnum.Red,
                                UpdateDateStamp = DateTime.Parse("02/05/2020")
                            },
                            new MyChildEntity()
                            {
                                MyChildKey = new Guid(ChildOfBbbbKeyPrefix + Convert.ToString(++childBCounter)),
                                MyParentUuidFk = bbbbGuid,
                                MyChildName = ChildOfBbbbNamePrefix + Convert.ToString(childBCounter),
                                MyChildMagicStatus = MagicStatusValues.Six,
                                FavoriteColor = Domain.Enums.FavoriteColorEnum.Yellow,
                                UpdateDateStamp = DateTime.Parse("02/29/2020")
                            }
                        }
                    },
                    new MyParentEntity
                    {
                        MyParentKey = ccccGuid,
                        MyParentName = "ParentC",
                        UpdateDateStamp = parentUpdateDateSeed.AddDays(++parentCreateCounter),
                        MyChilds = new List<MyChildEntity>
                        {
                            new MyChildEntity()
                            {
                                MyChildKey = new Guid(ChildOfCcccKeyPrefix + Convert.ToString(++childCCounter)),
                                MyParentUuidFk = ccccGuid,
                                MyChildName = ChildOfCcccNamePrefix + Convert.ToString(childCCounter),
                                MyChildMagicStatus = MagicStatusValues.NinetyEight,
                                FavoriteColor = Domain.Enums.FavoriteColorEnum.Red,
                                UpdateDateStamp = DateTime.Parse("02/29/2020")
                            }
                        }
                    },
                    new MyParentEntity
                    {
                        MyParentKey = ddddGuid,
                        MyParentName = "ParentD",
                        UpdateDateStamp = parentUpdateDateSeed.AddDays(++parentCreateCounter)
                        /* no child on purpose */
                    },
                    new MyParentEntity
                    {
                        MyParentKey = eeeeGuid,
                        MyParentName = "ParentE",
                        UpdateDateStamp = parentUpdateDateSeed.AddDays(++parentCreateCounter),
                        MyChilds = new List<MyChildEntity>
                        {
                            new MyChildEntity()
                            {
                                MyChildKey = new Guid(ChildOfEeeeKeyPrefix + Convert.ToString(++childECounter)),
                                MyParentUuidFk = eeeeGuid,
                                MyChildName = ChildOfEeeeNamePrefix + Convert.ToString(childECounter),
                                MyChildMagicStatus = MagicStatusValues.NinetyNine,
                                FavoriteColor = Domain.Enums.FavoriteColorEnum.Yellow,
                                /* this is a legit 99, but TOO FAR in the future */
                                UpdateDateStamp = DateTime.Parse("12/31/2020")
                            }
                        }
                    });

                context.SaveChanges();
            }
        }
    }
}